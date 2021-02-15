using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private ItemUI itemUIPrefab;
    public ItemUI currentItemUI;
    public Inventory inventory;
    public int index;

    public void AddItem(Item newItem)
    {
        if (currentItemUI == null)
        {
            currentItemUI = Instantiate(itemUIPrefab);
            currentItemUI.transform.SetParent(transform, false);
            currentItemUI.SetItem(newItem);
            currentItemUI.SetSlot(this);
        }
    }

    public void ClearSlot()
    {
        if (currentItemUI != null)
        {
            Destroy(currentItemUI.gameObject);
            currentItemUI = null;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            ItemUI itemUI = eventData.pointerDrag.GetComponent<ItemUI>();
            if(itemUI != null)
            {
                if (currentItemUI == null)
                {
                    inventory.Move(itemUI.item, index);
                    itemUI.SetSlot(this);
                }
                else
                {
                    itemUI.SetSlot(itemUI.currentSlot);
                } //else

            }// if ItemUI null
            GameManager.instance.dropSection.Hide();
        }
    }
}
