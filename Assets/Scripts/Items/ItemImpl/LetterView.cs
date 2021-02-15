using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterView : MonoBehaviour
{
    public void Initialize()
    {
        Canvas canvas = GameObject.Find("LetterViewCanvas").GetComponent<Canvas>();
        RectTransform letterRectTransform = GetComponent<RectTransform>();
        letterRectTransform.SetParent(canvas.transform, false);
        letterRectTransform.anchorMin = Vector2.zero;
        letterRectTransform.anchorMax = Vector2.one;
        letterRectTransform.offsetMin = Vector2.zero;
        letterRectTransform.offsetMax = Vector2.zero;
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
