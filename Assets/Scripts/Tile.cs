using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private TileData _tileData;
    private TileManager _tileManager;
    public int nbRessources;
    public RessourcesData currentRessource;
    [SerializeField] private TMP_Text _tileTxt;
    void Start()
    {
        _tileData = GetComponent<TileData>();
        _tileManager = FindObjectOfType<TileManager>();
    }

    void Update()
    {
      
    }

    void OnMouseDown()
    {
        _tileManager.selectedTile = this;

        if (_tileData.isEmpty)
        {
            _tileManager.PanelStatus(true);
        }
        else
        {
            nbRessources++;
        }
    }

    public void SetRessource(string ressource)
    {
        _tileTxt.gameObject.SetActive(true);
        _tileData.ressourceStatus = ressource;
        _tileData.isEmpty = false;
        this.gameObject.GetComponent<Renderer>().material = currentRessource.ressourceMat;
    }

}
