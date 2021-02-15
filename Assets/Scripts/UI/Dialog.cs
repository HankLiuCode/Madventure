using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public Canvas dialogCanvas;
    public TextMeshProUGUI dialogText;

    public DialogList dialogList;
    public List<int> dialogIndexes;

    private void Start()
    {
        dialogCanvas.gameObject.SetActive(false);
        foreach(int dialogIndex in dialogIndexes)
        {
            if(dialogIndex > dialogList.dialogs.Count)
            {
                Debug.LogError("dialogIndexes out of range!");
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        int dialogIndex = dialogIndexes[Random.Range(0, dialogIndexes.Count)];
        Character character = collision.GetComponent<Character>();
        if(character != null)
        {
            Show(dialogList.GetByIndex(dialogIndex));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Character character = collision.GetComponent<Character>();
        if (character != null)
        {
            Hide();
        }
    }

    public void Show(string dText)
    {
        dialogCanvas.gameObject.SetActive(true);
        dialogText.text = dText;
    }

    public void Hide()
    {
        dialogCanvas.gameObject.SetActive(false);
    }
}
