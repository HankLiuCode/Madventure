using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NPCData
{
    public int[] endProductKeys;
    public int[] ingredientKeys;
    public NPCData(NPC npc)
    {
        ingredientKeys = new int[npc.ingredient.space];
        for (int i = 0; i < ingredientKeys.Length; i++)
        {
            Item item = npc.ingredient.items[i];
            if (item != null)
            {
                ingredientKeys[i] = item.id;
            }
        }

        endProductKeys = new int[npc.endProduct.space];
        for (int i = 0; i < endProductKeys.Length; i++)
        {
            Item item = npc.endProduct.items[i];
            if (item != null)
            {
                endProductKeys[i] = item.id;
            }
        }
    }
}
