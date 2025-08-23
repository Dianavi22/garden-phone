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
                    selectedTile = null;
                    GameManager.Instance.HideAllPanels();
                    return;
                }
            }
        }
        selectedTile.SetRessource(ressource);
        selectedTile = null;
        GameManager.Instance.HideAllPanels();

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
        if (myTile != null && !myTile.VerifEmpty())
        {
            GardenManager.Instance.ResetTileCallBack(myTile.currentRessource, 1,  () => { });
            myTile.currentRessource.ressourceBasket.ressourceInBasket += myTile.nbRessources;
            myTile.CleanTile();
            myTile.GetComponent<Renderer>().material = _initMat;
            myTile.UpdateTextTile();
            _cm.SellClickerAssociate(myTile, () => {});
        }
        EventManager.Instance.Unsubscribe<OnTileSelected>(ReinitTile);
        SelectionManager.Instance.ActiveSelectionMode(false);
        GameManager.Instance.HideAllPanels();
    }

    public void ActiveSelectMode(int function)
    {
        GameManager.Instance.HideAllPanels();
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
