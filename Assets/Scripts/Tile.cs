using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tile : MonoBehaviour
{
    private TileManager _tileManager;
    public int nbRessources;
    public RessourcesData currentRessource;
    [SerializeField] private TMP_Text _tileTxt;
    [SerializeField] Material ActiveTileMat;
    [SerializeField] Material DisableTileMat;
    public bool isTileActive;
    private bool _isEmpty;
    private string ressourceStatus;
    [SerializeField] public int maxNbRessources;
    [SerializeField] public int idCamMode;

    void Start()
    {
        _tileManager = FindObjectOfType<TileManager>();
        _isEmpty = true;
    }

    public void OnMouseUp()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (GameManager.Instance.canClick && isTileActive)
        {
            if (SelectionManager.Instance.isGhostClick)
            {
                SelectionManager.Instance.isGhostClick = false;
                return;
            }

            _tileManager.selectedTile = this;

            if (_isEmpty)
            {
                _tileManager.PanelStatus(true);
            }
            else if (nbRessources < maxNbRessources)
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
        return _isEmpty;
    }
   
    public void CleanTile()
    {
        _isEmpty = true;
        nbRessources = 0;
        currentRessource = null;
        _tileTxt.gameObject.SetActive(false);
        ressourceStatus = null;

    }

    public void UpdateTextTile()
    {
        _tileTxt.text = nbRessources.ToString();
    }

    public void SetRessource(string ressource)
    {
        _tileTxt.gameObject.SetActive(true);
        ressourceStatus = ressource;
        _isEmpty = false;
        this.gameObject.GetComponent<Renderer>().material = currentRessource.ressourceMat;
    }

}
