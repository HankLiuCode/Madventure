using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public InventorySlot[] slots;

    private void Awake()
    {
        slots = GetComponentsInChildren<InventorySlot>();
        for (int i = 0; i < inventory.space; i++)
        {
            slots[i].index = i;
            slots[i].inventory = inventory;
        }
        inventory.onItemChangedCallback += UpdateUI;
    }
    public void UpdateUI()
    {
        for(int i=0; i<slots.Length; i++)
        {
            Item item = inventory.GetItem(i);
            if (item != null)
            {
                slots[i].AddItem(item);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
