using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemCookbook
{
    public List<ItemRecipe> recipes;

    public void AddReceipe(ItemRecipe recipe)
    {
        recipes.Add(recipe);
    }

    public Item GetEndProduct(Item[] items)
    {
        for(int i=0; i<recipes.Count; i++)
        {
            if(recipes[i].CheckIngredient(items))
            {
                return recipes[i].endProduct;
            }
        }
        return null;
    }
}
