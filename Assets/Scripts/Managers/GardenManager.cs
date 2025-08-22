using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
   [SerializeField] private List<GameObject> _gardenTile;
    [SerializeField] GameObject _shopPanel;
    [SerializeField] int _tileCost;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
   public void ShopShop()
    {
        _shopPanel.SetActive(true);
        GameManager.Instance.canClick = false;

    }

    public void AddNewTile()
    {
        GameManager.Instance.HideAllPanels();
        if (CoinsManager.Instance.coins >= _tileCost)
        {
            CoinsManager.Instance.Buy(_tileCost);
            for (int i = 0; i < _gardenTile.Count; i++)
            {
                if (!_gardenTile[i].activeSelf)
                {
                    _gardenTile[i].SetActive(true);
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
