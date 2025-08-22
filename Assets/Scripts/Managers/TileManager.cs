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
    [SerializeField] Material _initMat;
    [SerializeField] ClickerManager _cm;
    public bool isPanelActive = false;
    void Start()
    {
        _cm = GetComponent<ClickerManager>();
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
                if (CoinsManager.Instance.coins >= ressourcesList[i].cost)
                {
                    CoinsManager.Instance.Buy(ressourcesList[i].cost);
                    selectedTile.currentRessource = ressourcesList[i];
                    BasketManager.Instance.AddItemInBasket(ressourcesList[i]);
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

    //TODO Factoriser
    public void RessourceInBasket(object sender, OnTileSelected @event)
    {
        Tile myTile = SelectionManager.Instance.myTile;
        if (myTile != null && !myTile.VerifEmpty())
        {
            myTile.currentRessource.ressourceBasket.ressourceInBasket += myTile.nbRessources;
            myTile.nbRessources = 0;
            myTile.UpdateTextTile();
            BasketManager.Instance.UpdateItemInBasket(myTile.currentRessource);

        }
        SelectionManager.Instance.ActiveSelectionMode(false);
        GameManager.Instance.HideAllPanels();
        EventManager.Instance.Unsubscribe<OnTileSelected>(RessourceInBasket);
    }

    public void ReinitTile(object sender, OnTileSelected @event)
    {
        Tile myTile = SelectionManager.Instance.myTile;
        if (!myTile.VerifEmpty() && myTile != null)
        {
            myTile.currentRessource.ressourceBasket.ressourceInBasket += myTile.nbRessources;
            myTile.CleanTile();
            myTile.GetComponent<Renderer>().material = _initMat;
            myTile.UpdateTextTile();
             _cm.SellClicherAssociate(myTile, () => {});
        }
        EventManager.Instance.Unsubscribe<OnTileSelected>(ReinitTile);
        SelectionManager.Instance.ActiveSelectionMode(false);
        GameManager.Instance.HideAllPanels();
    }

    public void ActiveSelectMode(int function)
    {
        SelectionManager.Instance.ActiveSelectionMode(true);
        if(function == 0)
        {
            EventManager.Instance.Subscribe<OnTileSelected>(RessourceInBasket);
        }
        else
        {
            EventManager.Instance.Subscribe<OnTileSelected>(ReinitTile);
        }
    }
}
