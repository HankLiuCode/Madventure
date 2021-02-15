using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas canvas;
    public Image image;
    public Item item;
    public InventorySlot currentSlot;

    private float lastTimeClick;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        image = GetComponent<Image>();
    }

    private void Start()
    {
        canvas = GameObject.Find("InventoryCanvas").GetComponent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        GameManager.instance.dropSection.Show();
        canvasGroup.alpha = .5f;
        canvasGroup.blocksRaycasts = false;
        rectTransform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        Vector3 itemUiPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, 1));
        rectTransform.position = itemUiPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        SetSlot(currentSlot);
        GameManager.instance.dropSection.Hide();
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        float currentTimeClick = eventData.clickTime;
        if (Mathf.Abs(currentTimeClick - lastTimeClick) < 0.75f)
        {
            item.Use();
        }
        lastTimeClick = currentTimeClick;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Debug.Log($"Hovering over item {item}");
    }

    public void SetItem(Item item)
    {
        if (item == null)
        {
            this.item = null;
            image.sprite = null;
        }
        else
        {
            this.item = item;
            image.sprite = item.icon;
        }
    }
    public void SetSlot(InventorySlot slot)
    {
        currentSlot = slot;
        rectTransform.SetParent(slot.transform);
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
    }
}
