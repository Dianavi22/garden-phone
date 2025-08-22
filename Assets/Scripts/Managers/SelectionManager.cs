using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionManager : MonoBehaviour
{

    public bool isSelectedTileMode;
    private RaycastHit _hit;
    public Tile myTile;
    public bool isGhostClick;
    private Ray ray;
    public static SelectionManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isSelectedTileMode)
        {

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _hit, 100) && _hit.collider.CompareTag("Tile"))
            {
                myTile = _hit.transform.gameObject.GetComponent<Tile>();
                EventManager.Instance.SelectTile(myTile);
            }
            else
            {
                EventManager.Instance.SelectTile(myTile);
                ActiveSelectionMode(false);
            }
            myTile = null;
        }

    }

    public void ActiveSelectionMode(bool status)
    {
        isSelectedTileMode = status;
        GameManager.Instance.canClick = !status;
        isGhostClick = true;
    }
}
