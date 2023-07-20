using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class Inventory : ScriptableObject
{
    public List<InventorySlot> Items = new List<InventorySlot>();
    public void AddItem(ItemObject item, int amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].item == item)
            {
                Items[i].AddAmount(amount);
                hasItem = true;
                break;
            }
        }
        if(!hasItem)
        {
            Items.Add(new InventorySlot(item, amount));
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObject item;
    public int amounts;
    public InventorySlot(ItemObject item, int amounts)
    {
        this.item = item;
        this.amounts = amounts;
    }

    public void AddAmount(int value)
    {
        amounts += value;
    }
}
