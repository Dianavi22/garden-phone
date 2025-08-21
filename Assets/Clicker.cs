using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public Tile tileAssociate;
    private ClickerManager _clickerManager;

    private void Start()
    {
        _clickerManager = FindObjectOfType<ClickerManager>();
        _clickerManager.GiveTile(this);
    }
}
