using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 _baseTransform;
    public LayoutGroup gridLayout;
    private bool isDrop = false;
    private ItemBasket _itemBasket;
    private void Start()
    {
        _baseTransform = transform.localPosition;
        gridLayout = GetComponentInParent<LayoutGroup>();
        _itemBasket = GetComponent<ItemBasket>();

    }

    void Update()
    {
        if (isDrop)
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            foreach (RaycastResult result in results)
            {

                //TODO : Find a better solution
                if (result.gameObject.name == "SellRessourceButton")
                {
                    _itemBasket.SellItem();
                    isDrop = false;
                }
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        isDrop = true;
        transform.position = _baseTransform;
        LayoutRebuilder.ForceRebuildLayoutImmediate(gridLayout.GetComponent<RectTransform>());
    }

}
