using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class TileManager : MonoBehaviour
{
    public Tile selectedTile;
    [SerializeField] GameObject _panelSetTile;
    [SerializeField] List<RessourcesData> ressourcesList;
    [SerializeField] GameManager _gameManager;
    public bool isSelectedTileMode = false;
    private RaycastHit _hit;
    private Tile myTile;
    public bool isPanelActive = false;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isSelectedTileMode)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _hit, 100) && _hit.collider.CompareTag("Tile"))
            {
                myTile = _hit.transform.gameObject.GetComponent<Tile>();
                EventManager.Instance.SelectTile(myTile);
               
            }
            else
            {
                isSelectedTileMode = false;
            }

        }
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
        isPanelActive = status;
        _panelSetTile.SetActive(status);
    }

    public void RessourceInBasket(object sender, OnTileSelected @event)
    {
        if (!myTile.VerifEmpty())
        {
            myTile.currentRessource.ressourceBasket.ressourceInBasket += myTile.nbRessources;
            myTile.nbRessources = 0;
            myTile.UpdateTextTile();
            isSelectedTileMode = false;
        }
      
        EventManager.Instance.Unsubscribe<OnTileSelected>(RessourceInBasket);
        

    }

    public void ActiveSelectMode()
    {
        isSelectedTileMode = true;
        EventManager.Instance.Subscribe<OnTileSelected>(RessourceInBasket);
    }




}
