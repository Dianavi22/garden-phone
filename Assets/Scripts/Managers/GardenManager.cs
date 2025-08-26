using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    public List<Tile> gardenTile;
    public List<Tile> gardenActiveTile;
    [SerializeField] GameObject _shopPanel;

    [SerializeField] PricesData _priceData;
    [SerializeField] Material _noActiveMat;
    public static GardenManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    public void ResetTileCallBack(RessourcesData rd, int nbMinToReset, System.Action onComplete = null)
    {
        ResetItemInBasket(rd, nbMinToReset);
         onComplete?.Invoke();
    }
    private void ResetItemInBasket(RessourcesData rd, int nbMinToReset)
    {
        int nbOccuRessource = 0;
        for (int i = 0; i < gardenActiveTile.Count; i++)
        {
            if (!gardenActiveTile[i].VerifEmpty() && rd.title == gardenActiveTile[i].currentRessource.title)
            {
                nbOccuRessource++;
            }
        }
        if (nbOccuRessource <= nbMinToReset && rd.ressourceBasket.ressourceInBasket == 0)
        {
            BasketManager.Instance.DeleteItemBasket(rd);
        }
    }
    

    public void ShopShop()
    {
        GameManager.Instance.HideAllPanels();
        _shopPanel.SetActive(true);
        GameManager.Instance.canClick = false;
    }

    public void AddNewTile()
    {
        GameManager.Instance.HideAllPanels();
        if (CoinsManager.Instance.coins >= _priceData.tileCost)
        {
            CoinsManager.Instance.Buy(_priceData.tileCost);
            for (int i = 0; i < gardenTile.Count; i++)
            {
                if (gardenTile[i].idCamMode <= CameraManager.Instance.currentModeCam)
                {
                    gardenTile[i].gameObject.SetActive(true);
                    if (!gardenTile[i].isTileActive)
                    {
                        gardenTile[i].ChangeModeActiveTile(false);
                    }
                }
            }
            SelectionManager.Instance.ActiveSelectionMode(true);
            EventManager.Instance.Subscribe<OnTileSelected>(SelectNewTile);
        }
        else
        {
            CoinsManager.Instance.NoCoins();
        }
        GameManager.Instance.canClick = true;

    }

    private void SelectNewTile(object sender, OnTileSelected @event)
    {
        Tile myTile = SelectionManager.Instance.myTile;
        if (myTile != null && !myTile.isTileActive)
        {
            myTile.ChangeModeActiveTile(true);
        }
        HideTilesNoActive();
        EventManager.Instance.Unsubscribe<OnTileSelected>(SelectNewTile);
        SelectionManager.Instance.ActiveSelectionMode(false);
        GameManager.Instance.HideAllPanels();
    }

    public void AddCapacity()
    {
        GameManager.Instance.HideAllPanels();
        if (CoinsManager.Instance.coins >= _priceData.incraseCapacityTileCost)
        {
            CoinsManager.Instance.Buy(_priceData.incraseCapacityTileCost);
            SelectionManager.Instance.ActiveSelectionMode(true);
            EventManager.Instance.Subscribe<OnTileSelected>(IncreaseMaxCapacity);
        }
        GameManager.Instance.canClick = true;
    }

    private void IncreaseMaxCapacity(object sender, OnTileSelected @event)
    {
        Tile myTile = SelectionManager.Instance.myTile;
        if (myTile != null && myTile.isTileActive)
        {
            myTile.maxNbRessources += 20;
        }
        HideTilesNoActive();
        EventManager.Instance.Unsubscribe<OnTileSelected>(IncreaseMaxCapacity);
        SelectionManager.Instance.ActiveSelectionMode(false);
        GameManager.Instance.HideAllPanels();
    }

    private void HideTilesNoActive()
    {
        for (int i = 0; i < gardenTile.Count; i++)
        {
            if (!gardenTile[i].isTileActive)
            {
                gardenTile[i].gameObject.SetActive(false);
            }
        }
    }
}
