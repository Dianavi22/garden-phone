using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    public Tile selectedTile;
    [SerializeField] GameObject _panelSetTile;
    [SerializeField] List<RessourcesData> ressourcesList;
    [SerializeField] GameManager _gameManager;
    public bool isPanelActive = false;
    void Start()
    {

    }

    void Update()
    {
       
    }

    public void BackButton()
    {
        PanelStatus(false);
        selectedTile = null;
        GameManager.Instance.canClick = true;
    }

    public void SetTile(string ressource)
    {
        for (int i = 0; i < ressourcesList.Count; i++)
        {
            if (ressource == ressourcesList[i].title)
            {
                if (_gameManager.coins >= ressourcesList[i].cost)
                {
                    _gameManager.coins -= ressourcesList[i].cost;
                    _gameManager.UpdateCoinsText();
                    selectedTile.currentRessource = ressourcesList[i];
                }
                else
                {
                    BackButton();
                    return;
                }
            }
        }
        selectedTile.SetRessource(ressource);
        BackButton();
    }

    public void PanelStatus(bool status)
    {
        isPanelActive = status;
        _panelSetTile.SetActive(status);
    }

    public void RessourceInBasket(object sender, OnTileSelected @event)
    {
        Tile myTile = SelectionManager.Instance.myTile;
        if (!myTile.VerifEmpty())
        {
            myTile.currentRessource.ressourceBasket.ressourceInBasket += myTile.nbRessources;
            myTile.nbRessources = 0;
            myTile.UpdateTextTile();

        }
        SelectionManager.Instance.ActiveSelectionMode(false);
        GameManager.Instance.HideAllPanels();
        EventManager.Instance.Unsubscribe<OnTileSelected>(RessourceInBasket);
    }

    public void ActiveSelectMode()
    {
        SelectionManager.Instance.ActiveSelectionMode(true);
        EventManager.Instance.Subscribe<OnTileSelected>(RessourceInBasket);
    }




}
