using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int coins;
    [SerializeField] TMP_Text _coinsTxt;
    void Start()
    {
        UodateCoinsText();
    }

    void Update()
    {
    }

    public void UodateCoinsText()
    {
        _coinsTxt.text = coins.ToString();

    }


}
