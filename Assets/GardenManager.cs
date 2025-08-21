using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
   [SerializeField] private List<GameObject> _gardenTile;
    [SerializeField] GameObject _shopPanel;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
   public void ShopShop()
    {
        _shopPanel.SetActive(true);
    }

    public void AddNewTile()
    {
        GameManager.Instance.HideAllPanels();
        for (int i = 0; i < _gardenTile.Count; i++)
        {
            if (!_gardenTile[i].activeSelf)
            {
                _gardenTile[i].SetActive(true);
                return; 
            }
        }
    }
}
