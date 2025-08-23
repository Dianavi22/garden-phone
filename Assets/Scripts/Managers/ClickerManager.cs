using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerManager : MonoBehaviour
{

    [SerializeField] private GameObject _clicker;
    [SerializeField] private GardenManager _gardenManager;
    [SerializeField] private List<Clicker> _clickers;
    [SerializeField] private int _clickerCost;
    private Tile _myTile;
    public float interval = 1f;
    public int incrementAmount = 1;

    private void Start()
    {
        _gardenManager = FindAnyObjectByType<GardenManager>();
    }

    public void ActiveSelectMode()
    {
        GameManager.Instance.HideAllPanels();
        if (_clickerCost > CoinsManager.Instance.coins)
        {
            CoinsManager.Instance.NoCoins();
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
         _myTile = SelectionManager.Instance.myTile;
        print(_myTile);
        if (_myTile != null && !_myTile.VerifEmpty() )
        {
            Instantiate(_clicker, transform.position, Quaternion.identity);
            CoinsManager.Instance.Buy(_clickerCost);

        }
        SelectionManager.Instance.ActiveSelectionMode(false);
        GameManager.Instance.HideAllPanels();
        EventManager.Instance.Unsubscribe<OnTileSelected>(AddClicker);
    }

    public void GiveTile(Clicker clicker)
    {
        _clickers.Add(clicker);
        clicker.tileAssociate = _myTile;
    }

    public void SellClickerAssociate(Tile tile, System.Action onComplete = null)
    {
        int n = 0;
        for (int i = _clickers.Count - 1; i >= 0; i--)
        {
            if (_clickers[i].tileAssociate == tile)
            {
                Destroy(_clickers[i].gameObject);
                _clickers.RemoveAt(i);
                n++;
            }
        }
        CoinsManager.Instance.GetCoins(n, 3);
        onComplete?.Invoke(); 
    }
}
