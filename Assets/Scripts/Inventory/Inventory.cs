﻿using Cyberultimate;
using Cyberultimate.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoSingleton<Inventory>
{
	private Item[] Items { get; } = new Item[5];

	public Dictionary<string, ItemScriptableObject> AllGameItems { get; private set; } = new Dictionary<string, ItemScriptableObject>();

	public LockValue<uint> soil = new LockValue<uint>(10, 0, 0);
	public LockValue<uint> water = new LockValue<uint>(10, 0, 0);

	private void Start()
	{
		var items = Resources.LoadAll<ItemScriptableObject>("Items");
		foreach (var item in items)
		{
			AllGameItems[item.name] = item;
		}
	}

	public bool HasItem(string name)
	{
		return Items.Any(x => x != null && x.Name == name);
	}

	public Item GetItem(Cint slot)
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

			Items[i] = new Item(item);
			foundSlot = true;
			break;
		}

		InventoryUI.Instance.Refresh();

		if (!foundSlot)
		{
			UIManager.Instance.ShowPopupText("Inventory full!");
		}

		return foundSlot;
	}

	public void AddResource(uint count, PlantSystem.PlantResources resource)
	{
		if (resource == PlantSystem.PlantResources.Soil)
		{
			if (soil.Value == soil.Max)
			{
				UIManager.Instance.ShowPopupText("Not enough space for soil!");
			}
			else
			{
				soil.GiveValue(count, "");
				UIManager.Instance.ChangeResources(resource, soil.Value, soil.Max);
				UIManager.Instance.ShowPopupText("Collected soil");
			}
		}
		else if (resource == PlantSystem.PlantResources.Water)
		{
			if (water.Value == water.Max)
			{
				UIManager.Instance.ShowPopupText("Not enough space for water!");
			}
			else
			{
				water.GiveValue(count, "");
				UIManager.Instance.ChangeResources(resource, water.Value, water.Max);
				UIManager.Instance.ShowPopupText("Collected water");
			}
		}
	}

	public void DrainWater()
	{
		water.TakeValue(water.Value);
		UIManager.Instance.ChangeResources(PlantSystem.PlantResources.Water, water.Value, water.Max);
	}

	public void DrainSoil()
	{
		soil.TakeValue(soil.Value);
		UIManager.Instance.ChangeResources(PlantSystem.PlantResources.Soil, soil.Value, soil.Max);
	}

	public void RemoveItem(Cint slot, bool showPopup=false)
	{
        UIManager.Instance.ShowPopupText("Dropped " + Items[slot].name);

        Items[slot] = null;
		InventoryUI.Instance.Refresh();
	}

	public bool RemoveItemByName(string name)
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
}