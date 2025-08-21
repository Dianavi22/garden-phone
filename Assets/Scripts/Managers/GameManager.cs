using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int coins;
    [SerializeField] TMP_Text _coinsTxt;
    public bool canClick;

    [SerializeField] List<GameObject> _panelList;
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    void Start()
    {
        canClick = true;
        UpdateCoinsText();
    }

    void Update()
    {
    }
    public void HideAllPanels()
    {
        for (int i = 0; i < _panelList.Count; i++)
        {
            _panelList[i].SetActive(false);
        }
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


}
