using Cyberultimate;
using Cyberultimate.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoSingleton<Inventory>
{
	public Item[] Items { get; private set; } = new Item[5];

	public Dictionary<string, ItemScriptableObject> AllGameItems { get; private set; } = new Dictionary<string, ItemScriptableObject>();

	public LockValue<uint> Soil
	{
		get
		{
			Item it = GetItemCurrentlySelected();

			if (it?.Tag == "Shovel")
			{
				return it?.FillAmount;
			}

			return null;

		}
	}


	public LockValue<uint> Water
	{
		get
		{
			Item it = GetItemCurrentlySelected();

			if (it?.Tag == "Bottle")
			{
				return it?.FillAmount;
			}

			return null;
		}
	}


	private void Start()
	{
		var items = Resources.LoadAll<ItemScriptableObject>("Items");
		foreach (var item in items)
		{
			AllGameItems[item.name] = item;
		}
	}

	public bool HasItem(string keyword)
	{
		return Items.Any(x => x != null && (x.Name == keyword || x.Tag == keyword));
	}

	public bool CheckNotSelectedTrueItem (string neededItemTag, Item item = null)
	{
		return !GetAllItemsByTag(neededItemTag).Contains(item ?? Inventory.Instance.GetItemCurrentlySelected());
	}

	public Item GetItem(Cint slot)
	{
		if (slot >= Items.Length) return null;
		return Items[slot];
	}

	public Item GetItemCurrentlySelected ()
	{
		return Items[InventoryUI.Instance.SelectedSlot];
	}

	public Item GetItemByName(string name)
	{
		return Items.FirstOrDefault(x => x?.Name == name);
	}

	public Item[] GetAllItemsByTag(string tag)
	{
		return Items.Where(x => x?.Tag == tag).ToArray();
	}

	public bool AddItem(ItemScriptableObject item)
	{
		var foundSlot = false;
		for (var i = 0; i < Items.Length; i++)
		{
			if (Items[i] != null) continue;

			var it = new Item(item);

			if (it.Fillable)
			{
				it.FillAmount.OnValueChanged += (sender, e) => InventoryUI.Instance.Refresh();
			}
			 
			Items[i] = it;
			foundSlot = true;
			break;
		}

		InventoryUI.Instance.Refresh();

		if (!foundSlot)
		{
			UIManager.Instance.ShowPopupText("Inventory full!");
		}
		else
		{
			TaskManager.Instance.RemoveTask($"Find {item.name}");
			TaskManager.Instance.RemoveTask($"Find {item.tag}");
		}

		return foundSlot;
	}
	

	public void AddResource(uint count, PlantSystem.PlantResources resource)
	{
		
		if (resource == PlantSystem.PlantResources.Soil)
		{
			var soil = Soil;
			
			if (soil == null) return;
			
			if (soil.Value == soil.Max)
			{
				UIManager.Instance.ShowPopupText("Not enough space for soil!");
			}
			else
			{
				Soil.GiveValue(count, "");
				UIManager.Instance.ShowPopupText("Collected soil");
			}
		}
		else if (resource == PlantSystem.PlantResources.Water)
		{
			var water = Water;

			if (water == null) return;
			
			if (water.Value == water.Max)
			{
				UIManager.Instance.ShowPopupText("Not enough space for water!");
			}
			else
			{
				water.GiveValue(count, "");
				UIManager.Instance.ShowPopupText("Collected water");
			}
		}
	}

	public void DrainWater()
	{
		Water.TakeValue(Water.Value);
	}

	public void DrainSoil()
	{
		Soil.TakeValue(Soil.Value);
	}

	public void RemoveItem(Cint slot, bool showPopup = false)
	{
        if(showPopup && Items[slot] != null) UIManager.Instance.ShowPopupText($"Dropped {Items[slot].Name}");

        Items[slot] = null;
		InventoryUI.Instance.Refresh();
	}

	public bool RemoveItem(string name)
	{
		for (var i = 0; i < Items.Length; i++)
		{
			if (Items[i].Name == name)
			{
				Items[i] = null;
				return true;
			}
		}

		return false;
	}

	public void RemoveItem(Item item)
	{
		var found = Items.Select((x, i) => new {item = x, index = i}).FirstOrDefault(x => x.item == item);
		if (found != null) Items[found.index] = null;
	}
}