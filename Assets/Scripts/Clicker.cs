using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public Tile tileAssociate;
    private ClickerManager _clickerManager;
   

    private float timer;
    private int totalValue;
     private void Start()
    {
        _clickerManager = FindObjectOfType<ClickerManager>();
        _clickerManager.GiveTile(this);
    }

    void Update()
    {
        if (tileAssociate.nbRessources< tileAssociate.maxNbRessources)
        {
            timer += Time.deltaTime;

            if (timer >= _clickerManager.interval)
            {
                int ticks = Mathf.FloorToInt(timer / _clickerManager.interval);
                timer -= ticks * _clickerManager.interval;

                tileAssociate.nbRessources += _clickerManager.incrementAmount * ticks;
                tileAssociate.UpdateTextTile();
            }
        }
       
    }

    public int GetValue()
    {
        return totalValue;
    }
   
}
