using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerManager : MonoBehaviour
{

    [SerializeField] private GameObject _clicker;
    [SerializeField] private GardenManager _gardenManager;
    [SerializeField] private List<Clicker> _clickers;
    [SerializeField] private int _clickerCost;
    public float interval = 1f;
    public int incrementAmount = 1;

    private void Start()
    {
        _gardenManager = FindAnyObjectByType<GardenManager>();
    }

    public void ActiveSelectMode()
    {
        GameManager.Instance.HideAllPanels();
        if (_clickerCost > GameManager.Instance.coins)
        {
            GameManager.Instance.NoCoins();
            GameManager.Instance.canClick = true;

            return;
        }
        else
        {
            SelectionManager.Instance.ActiveSelectionMode(true);
            EventManager.Instance.Subscribe<OnTileSelected>(AddClicker);
        }
            
    }
    public void AddClicker(object sender, OnTileSelected @event)
    {
        if (!SelectionManager.Instance.myTile.VerifEmpty())
        {
            Instantiate(_clicker, transform.position, Quaternion.identity);
            GameManager.Instance.Buy(_clickerCost);

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
