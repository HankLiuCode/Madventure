using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int space = 5;
    public Item[] items;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public delegate void OnInventoryFull();
    public OnInventoryFull onInventoryFullCallback;


    private void Awake()
    {
         items = new Item[space];
    }

    public bool IsFull()
    {
        for (int i = 0; i < space; i++)
        {
            if (items[i] == null)
            {
                return false;
            }
        }
        onInventoryFullCallback?.Invoke();
        return true;
    }

    public bool IsEmpty()
    {
        for (int i = 0; i < space; i++)
        {
            if (items[i] != null) 
            {
                return false;
            }
        }
        return true;
    }

    public Item GetItem(int index)
    {
        return items[index];
    }

    public int GetIndex(Item item)
    {
        for (int i = 0; i < space; i++)
        {
            if (items[i] == item)
            {
                return i;
            }
        }
        return -1;
    }

    public void Add(Item item)
    {
        for(int i=0; i<space; i++)
        {
            if(items[i] == null)
            {
                items[i] = item;
                if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
                return;
            }
        }
        Debug.Log("Inventory Is Full");
    }

    public void Add(Item item, int index)
    {
        if(items[index] == null)
        {
            items[index] = item;
            if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
        }
        else
        {
            Debug.Log($"Failed to add item {item}: Item exists in index {index}");
        }
    }


    public void Remove(Item item)
    {
        for (int i = 0; i < space; i++)
        {
            if (items[i] == item)
            {
                items[i] = null;
                if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
                return;
            }
        }
        Debug.Log($"Failed to find item {item}");
    }

    public void Remove(int index)
    {
        items[index] = null;
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
    }

    public void Move(Item item, int to)
    {
        for (int i = 0; i < space; i++)
        {
            if (items[i] == item)
            {
                Remove(item);
                Add(item, to);
                return;
            }
        }
    }

    public void Swap(int from, int to)
    {
        Item temp = items[from];
        items[from] = items[to];
        items[to] = temp;
    }

    public void Clear()
    {
        for (int i = 0; i < space; i++)
        {
            items[i] = null;
        }
        if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
    }
}
