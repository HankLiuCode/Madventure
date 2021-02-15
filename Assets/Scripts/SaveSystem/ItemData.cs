using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public float[] itemPosition;
    public int id;

    public ItemData(Item item)
    {
        itemPosition = new float[3];
        itemPosition[0] = item.transform.position.x;
        itemPosition[1] = item.transform.position.y;
        itemPosition[2] = item.transform.position.z;
        id = item.id;
    }
}
