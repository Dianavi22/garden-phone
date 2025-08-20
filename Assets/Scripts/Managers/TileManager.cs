using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public Tile selectedTile;
    [SerializeField] GameObject _panelSetTile;
    [SerializeField] List<RessourcesData> ressourcesList;
    [SerializeField] GameManager _gameManager;
    void Start()
    {

    }

    void Update()
    {

    }

    public void BackButton()
    {
        PanelStatus(false);
        selectedTile = null;
    }

    public void SetTile(string ressource)
    {
        for (int i = 0; i < ressourcesList.Count; i++)
        {
            if (ressource == ressourcesList[i].title)
            {
                if (_gameManager.coins >= ressourcesList[i].cost)
                {
                    _gameManager.coins -= ressourcesList[i].cost;
                    _gameManager.UodateCoinsText();
                    selectedTile.currentRessource = ressourcesList[i];
                }
                else
                {
                    BackButton();
                    return;
                }
            }
        }
        selectedTile.SetRessource(ressource);
        BackButton();
    }

    public void PanelStatus(bool status)
    {
        _panelSetTile.SetActive(status);
    }

    public void RessourceInBasket(Tile myTile)
    {
        // Event Wait Click
        myTile.currentRessource.ressourceBasket.ressourceInBasket += myTile.nbRessources;
        myTile.nbRessources = 0;
        print(myTile.currentRessource.ressourceBasket.ressourceInBasket);
    }
}
