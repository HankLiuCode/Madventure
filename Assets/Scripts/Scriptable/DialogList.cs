using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "List/Dialog")]
public class DialogList : ScriptableObject
{
    public List<string> dialogs;

    public string GetByIndex(int index)
    {
        return dialogs[index].Replace("\\n","\n");
    }
}
