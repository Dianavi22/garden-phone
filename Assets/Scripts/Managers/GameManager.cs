using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   
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
  


}
