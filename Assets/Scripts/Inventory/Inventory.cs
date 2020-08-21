using Cyberultimate;
using Cyberultimate.Unity;
using UnityEngine;

public class Inventory : MonoSingleton<Inventory>
{
    private Item[] Items { get; } = new Item[5];

    public Item GetItem(Cint slot)
    {
        if (slot >= Items.Length) return null;
        return Items[slot];
    }
    
    public bool AddItem(Item item)
    {
        var foundSlot = false;
        for (var i = 0; i < Items.Length; i++)
        {
            if (Items[i] != null) continue;
            
            Items[i] = item;
            foundSlot = true;
            break;
        }

        InventoryUI.Instance.Refresh();
        return foundSlot;
    }

    public void Remove(Cint slot)
    {
        Items[slot] = null;
        InventoryUI.Instance.Refresh();
    }
}