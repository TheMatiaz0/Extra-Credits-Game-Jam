using System;
using System.Collections.Generic;
using System.Linq;
using Cyberultimate;
using Cyberultimate.Unity;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoSingleton<Inventory>
{
    private ItemScriptableObject[] Items { get; } = new ItemScriptableObject[5];
    
    public Dictionary<string, ItemScriptableObject> AllGameItems { get; private set; } = new Dictionary<string, ItemScriptableObject>();

    private void Start()
    {
        var items = Resources.LoadAll<ItemScriptableObject>("Items");
        foreach (var item in items)
        {
            AllGameItems[item.name] = item;
        }
        
        Debug.Log($"Loaded {items.Length} items: {string.Join(", ", (IEnumerable<ItemScriptableObject>)items)}");
    }

    public bool HasItem(string name)
    {
        return Items.Any(x => x != null && x.name == name);
    }
    
    public ItemScriptableObject GetItem(Cint slot)
    {
        if (slot >= Items.Length) return null;
        return Items[slot];
    }
    
    public bool AddItem(ItemScriptableObject item)
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