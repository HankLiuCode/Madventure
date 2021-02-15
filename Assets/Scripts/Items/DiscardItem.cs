using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiscardItem : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ItemUI itemUI = eventData.pointerDrag.GetComponent<ItemUI>();
            Character player = GameObject.Find("Player").GetComponent<Character>();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
            bool dropRight = (mousePos.x - player.transform.position.x) > 0 ? true : false;
            Debug.Log("mousePosition: " + eventData.position + "player Position" + player.transform.position);
            player.DropItem(itemUI.item, dropRight);
            itemUI.SetSlot(itemUI.currentSlot);
        }
    }
}
