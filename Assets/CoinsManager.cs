using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public int coins;
    [SerializeField] TMP_Text _coinsTxt;
    public static CoinsManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    void Start()
    {
        UpdateCoinsText();
    }

    void Update()
    {
        
    }

    public void UpdateCoinsText()
    {
        _coinsTxt.text = coins.ToString();

    }
    public void Buy(int cost)
    {
        coins -= cost;
        UpdateCoinsText();
    }

    public void NoCoins()
    {
        print("NO COINS");
    }

    public void GetCoins(int nbToSell, int price)
    {
        coins += nbToSell * price;
        UpdateCoinsText();
    }
}
