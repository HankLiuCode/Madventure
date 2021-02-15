using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class ItemsData
{
    public List<ItemData> itemDataList;

    public ItemsData(Item[] items)
    {
        itemDataList = new List<ItemData>();
        foreach(Item item in items)
        {
            itemDataList.Add(new ItemData(item));
        }
    }
}
