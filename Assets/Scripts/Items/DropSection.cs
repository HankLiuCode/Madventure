using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropSection : MonoBehaviour, IDropHandler
{
    CanvasGroup canvasGroup;

    private void Start()
    {
        canvasGroup = GetComponentInParent<CanvasGroup>();
        Hide();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            ItemUI itemUI = eventData.pointerDrag.GetComponent<ItemUI>();
            Character player = GameObject.Find("Player").GetComponent<Character>();
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(eventData.position);
            bool dropRight = (mousePos.x - player.transform.position.x) > 0 ? true : false;
            player.DropItem(itemUI.item, dropRight);
            itemUI.SetSlot(itemUI.currentSlot);
            Hide();
            // Debug.Log("mousePosition: " + eventData.position + "player Position" + player.transform.position);
        }
    }

    public void Show()
    {
        canvasGroup.alpha = 1f;
    }

    public void Hide()
    {
        canvasGroup.alpha = 0f;
    }

}
