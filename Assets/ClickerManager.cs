using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerManager : MonoBehaviour
{

    [SerializeField] private GameObject _clicker;
    [SerializeField] private GardenManager _gardenManager;
    [SerializeField] private List<Clicker> _clickers;

    private void Start()
    {
        _gardenManager = FindAnyObjectByType<GardenManager>();
    }

    public void ActiveSelectMode()
    {
        SelectionManager.Instance.ActiveSelectionMode(true);
        GameManager.Instance.HideAllPanels();

        EventManager.Instance.Subscribe<OnTileSelected>(AddClicker);
    }
    public void AddClicker(object sender, OnTileSelected @event)
    {
        if (!SelectionManager.Instance.myTile.VerifEmpty())
        {
            Instantiate(_clicker, transform.position, Quaternion.identity);
        }
        SelectionManager.Instance.ActiveSelectionMode(false);
        GameManager.Instance.HideAllPanels();
        EventManager.Instance.Unsubscribe<OnTileSelected>(AddClicker);
    }

    public void GiveTile(Clicker clicker)
    {
        _clickers.Add(clicker);
        clicker.tileAssociate = SelectionManager.Instance.myTile;
    }
}
