using Cyberultimate;
using Cyberultimate.Unity;
using UnityEngine;

public class Inventory : MonoSingleton<Inventory>
{
    private Item[] items = new Item[5];

    public Item GetItem(Cint slot)
    {
        if (slot >= items.Length) return null;
        return items[slot];
    }
    
    public bool AddItem(Item item)
    {
        var foundSlot = false;
        for (var i = 0; i < items.Length; i++)
        {
            if (items[i] != null) continue;
            
            items[i] = item;
            foundSlot = true;
            break;
        }

        InventoryUI.Instance.Refresh();
        return foundSlot;
    }

    public void Remove(Cint slot)
    {
        items[slot] = null;
        InventoryUI.Instance.Refresh();
    }
}