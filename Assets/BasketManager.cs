using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketManager : MonoBehaviour
{

    [SerializeField]  private List<ItemBasket> _itemsInBasket = new List<ItemBasket>();
    [SerializeField] GameObject _buttonBasket;
    [SerializeField] GameObject _itemBasket;
    [SerializeField] GameObject _basketGrid;
    public static BasketManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }
    void Update()
    {
        
    }

    public void ShowBasket()
    {
        _buttonBasket.SetActive(!_buttonBasket.activeSelf);
    }

    public void UpdateItemInBasket(RessourcesData rd)
    {
        for (int i = 0; i < _itemsInBasket.Count; i++)
        {
            if(rd == _itemsInBasket[i].dataAssociate)
            {
                _itemsInBasket[i].myText.text = rd.ressourceBasket.ressourceInBasket.ToString();
                return;
            }
        }
    }

    public void AddItemInBasket(RessourcesData rd)
    {
        if (_itemsInBasket.Count != 0)
        {
            for (int i = 0; i < _itemsInBasket.Count; i++)
            {
                if (rd.title == _itemsInBasket[i].dataAssociate.title)
                {
                    return;
                }
            }
        }
       var newItem = Instantiate(_itemBasket, transform.position, Quaternion.identity);
        newItem.GetComponent<ItemBasket>().dataAssociate = rd;
       _itemsInBasket.Add(newItem.GetComponent<ItemBasket>());
        newItem.GetComponent<Image>().color = rd.ressourceBasket.colorBasketItem;
        newItem.transform.parent = _basketGrid.transform;
        newItem.transform.localScale = new Vector3(1.5f, 1, 1);

    }

    public void DeleteItemBasket()
    {

    }
}
