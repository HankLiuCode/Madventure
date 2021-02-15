using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Letter : Item
{
    public GameObject letterViewPrefab;
    private LetterView letterView;

    public override void Use()
    {
        if(letterView == null)
        {
            letterView = Instantiate(letterViewPrefab).GetComponent<LetterView>();
            letterView.Initialize();
            letterView.Show();
        }
        else
        {
            letterView.Show();
        }
    }

    public void Close()
    {
        if(letterView != null)
        {
            letterView.Hide();
        }
    }
}
