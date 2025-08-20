using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GardenManager : MonoBehaviour
{
   [SerializeField] private List<GameObject> _gardenTile;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddNewTile()
    {
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
