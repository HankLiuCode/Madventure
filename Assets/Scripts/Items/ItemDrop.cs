using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrop : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("ItemDrop");
        if (eventData.pointerDrag != null)
        {
            ItemUI itemUI = eventData.pointerDrag.GetComponent<ItemUI>();
            Character player = GameObject.Find("Player").GetComponent<Character>();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
            bool dropRight = (mousePos.x - player.transform.position.x) > 0 ? true : false;
            player.DropItem(itemUI.item, dropRight);
            itemUI.SetSlot(itemUI.currentSlot);

            // Debug.Log("mousePosition: " + eventData.position + "player Position" + player.transform.position);
        }
    }
}
