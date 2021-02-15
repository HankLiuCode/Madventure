using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemRecipe
{
    public List<Item> ingredient;
    public Item endProduct;

    public ItemRecipe(List<Item> _ingredient, Item _endProduct)
    {
        ingredient = _ingredient;
        ingredient.Sort();
        endProduct = _endProduct;
    }
    public ItemRecipe(Item[] _ingredient, Item _endProduct)
    {
        ingredient = new List<Item>(_ingredient);
        ingredient.Sort();
        endProduct = _endProduct;
    }

    public bool Equals(ItemRecipe recipe)
    {
        for(int i=0; i<recipe.ingredient.Count; i++)
        {
            if(recipe.ingredient[i].id != ingredient[i].id)
            {
                return false;
            }
        }
        return true;
    }

    public bool CheckIngredient(Item[] items)
    {
        for (int i = 0; i < ingredient.Count; i++)
        {
            if(items[i] == null)
            {
                return false;
            }
            else if (ingredient[i].id != items[i].id)
            {
                return false;
            }
        }
        return true;
    }
}
