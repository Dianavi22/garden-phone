using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerManager : MonoBehaviour
{
    public bool isSelectedTileMode;
    private RaycastHit _hit;
    private Tile myTile;
    [SerializeField] private GameObject _clicker;
    [SerializeField] private GardenManager _gardenManager;
    [SerializeField] private List<Clicker> _clickers;

    private void Start()
    {
        _gardenManager = FindAnyObjectByType<GardenManager>();
    }
    private void Update()
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
    public void ActiveSelectMode()
    {
        isSelectedTileMode = true;
        _gardenManager.ShowShop(false);
        EventManager.Instance.Subscribe<OnTileSelected>(AddClicker);
    }
    public void AddClicker(object sender, OnTileSelected @event)
    {
        Instantiate(_clicker, transform.position, Quaternion.identity);
        EventManager.Instance.Unsubscribe<OnTileSelected>(AddClicker);
    }

    public void GiveTile(Clicker clicker)
    {
        _clickers.Add(clicker);
        clicker.tileAssociate = myTile;
    }
}
