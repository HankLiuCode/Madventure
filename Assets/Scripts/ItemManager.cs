using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public ItemList itemList;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Debug.Log("Instance already exists");
            Destroy(this);
        }
    }

    private void Start()
    {
        Load();
    }

    public void Save()
    {
        Item[] items = FindObjectsOfType<Item>();
        List<Item> activeItems = new List<Item>();
        foreach(Item item in items)
        {
            if (item.gameObject.activeInHierarchy)
            {
                activeItems.Add(item);
            }
        }
        SaveSystem.SaveWorldItems(activeItems.ToArray());
    }
    public void Load()
    {
        ItemsData data = SaveSystem.LoadWorldItems();
        if(data != null)
        {
            Item[] items = FindObjectsOfType<Item>();
            foreach (Item item in items)
            {
                Destroy(item.gameObject);
            }

            foreach (ItemData itemData in data.itemDataList)
            {
                GameObject itemPrefab = itemList.items[itemData.id].gameObject;
                Vector3 itemPosition = new Vector3(itemData.itemPosition[0], itemData.itemPosition[1], itemData.itemPosition[2]);
                Instantiate(itemPrefab, itemPosition, Quaternion.identity);
            }
        }
    }
}
