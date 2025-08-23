using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
    public List<GameObject> gardenTile;
    public List<Tile> gardenActiveTile;
    [SerializeField] GameObject _shopPanel;
    [SerializeField] int _tileCost;
    public static GardenManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    void Start()
    {

    }

    void Update()
    {

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
        if (CoinsManager.Instance.coins >= _tileCost)
        {
            CoinsManager.Instance.Buy(_tileCost);
            for (int i = 0; i < gardenTile.Count; i++)
            {
                if (!gardenTile[i].activeSelf)
                {
                    gardenTile[i].SetActive(true);
                    gardenActiveTile.Add(gardenTile[i].GetComponent<Tile>());
                    break;
                }
            }
        }
        else
        {
            CoinsManager.Instance.NoCoins();

        }
        GameManager.Instance.canClick = true;

    }
}
