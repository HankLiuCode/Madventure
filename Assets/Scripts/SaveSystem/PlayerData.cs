using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float[] playerPosition;
    public int[] itemKeys;

    public PlayerData(Character character)
    {
        playerPosition = new float[3];
        playerPosition[0] = character.transform.position.x;
        playerPosition[1] = character.transform.position.y;
        playerPosition[2] = character.transform.position.z;

        itemKeys = new int[character.inventory.space];
        for(int i=0; i<itemKeys.Length; i++)
        {
            Item item = character.inventory.items[i];
            if(item != null)
            {
                itemKeys[i] = item.id;
            }
        }
    }
}
