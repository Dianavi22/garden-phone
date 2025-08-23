using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemBasket : MonoBehaviour
{
    public RessourcesData dataAssociate;
    public TMP_Text myText;
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void SellItem()
    {
        CoinsManager.Instance.GetCoins(dataAssociate.ressourceBasket.ressourceInBasket, dataAssociate.price);
        dataAssociate.ressourceBasket.ressourceInBasket = 0;
        myText.text = dataAssociate.ressourceBasket.ressourceInBasket.ToString();
        GardenManager.Instance.ResetTileCallBack(dataAssociate, 0, () => { });
    }

}
