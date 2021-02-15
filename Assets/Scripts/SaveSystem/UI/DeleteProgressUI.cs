using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DeleteProgressUI : MonoBehaviour
{
    public TextMeshProUGUI btnTextMesh;
    public TextMeshProUGUI clickLeftTextMesh;
    public int clickLeft = 5;

    private void Start()
    {
        clickLeftTextMesh.text = $"Click {clickLeft} times to delete.";
    }
    public void DeleteProgress()
    {
        if (clickLeft >= 1)
        {
            clickLeft -= 1;
            clickLeftTextMesh.text = $"Click {clickLeft} times to delete.";
        }

        if (clickLeft <= 0)
        {
            SaveSystem.DeleteProgress();
            btnTextMesh.text = "Progress Deleted";
        }
    }
}
