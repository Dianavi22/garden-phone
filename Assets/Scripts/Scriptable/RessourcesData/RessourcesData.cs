using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RessourcesData : ScriptableObject
{
    public int id;
    public string title;
    public int cost;
    public int price;
    public Material ressourceMat;
    public BasketDataManager ressourceBasket;
}
