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
    [SerializeField] Material ActiveTileMat;
    [SerializeField] Material DisableTileMat;
    public bool isTileActive;
    void Start()
    {
        _tileData = GetComponent<TileData>();
        _tileManager = FindObjectOfType<TileManager>();

    }

    void Update()
    {

    }

    void OnMouseUp()
    {
        if (GameManager.Instance.canClick && isTileActive)
        {
            if (SelectionManager.Instance.isGhostClick)
            {
                SelectionManager.Instance.isGhostClick = false;
                return;
            }
            _tileManager.selectedTile = this;
            if (_tileData.isEmpty)
            {
                _tileManager.PanelStatus(true);
            }
            else
            {
                nbRessources++;
                UpdateTextTile();
            }
        }

    }

    public void ChangeModeActiveTile(bool isActive)
    {
        if (isActive)
        {
            isTileActive = true;
            this.GetComponent<Renderer>().material = ActiveTileMat;

        }
        else
        {
            isTileActive = false;
            this.GetComponent<Renderer>().material = DisableTileMat;
        }
    }


    public bool VerifEmpty()
    {
        return _tileData.isEmpty;
    }

    public void CleanTile()
    {
        _tileData.isEmpty = true;
        nbRessources = 0;
        currentRessource = null;
        _tileTxt.gameObject.SetActive(false);
        _tileData.ressourceStatus = null;

    }

    public void UpdateTextTile()
    {
        _tileTxt.text = nbRessources.ToString();
    }

    public void SetRessource(string ressource)
    {
        _tileTxt.gameObject.SetActive(true);
        _tileData.ressourceStatus = ressource;
        _tileData.isEmpty = false;
        this.gameObject.GetComponent<Renderer>().material = currentRessource.ressourceMat;
    }

}
