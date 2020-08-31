using Cyberultimate.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Garbage : InteractableObject
{
	public override string InteractionName => "Search garbage";
	public GarbageManager manager;

	public bool garbageUsed = false;
	public bool garbageOpen = false;
	public float garbageDropChance = 50;

	private void Start()
	{
		interactionTime = 3f;
	}

	public override void KeyDown()
	{
		AudioManager.Instance.PlaySFX(manager.garbage1 ? "garbage1" : "garbage2");
	}

	protected override void OnInteract()
	{
		if (garbageUsed) return;
		if (!manager.firstItemUsed) { FirstItem(); return; }

		manager.garbage1 = !manager.garbage1;

		int garbageRnd = Random.Range(0, 100);
		if (garbageRnd <= garbageDropChance)
		{
			UIManager.Instance.ShowPopupText("You found only garbage");
			garbageUsed = true;
			Destroy(this);
		}
		else
		{
			int rnd = Random.Range(0, manager.itemDrops.Count);
			var item = manager.itemDrops[rnd];
			if (Inventory.Instance.AddItem(item))
			{
				UIManager.Instance.ShowPopupText($"You found a {item.name}");

				if (item.oneTimeLoot) manager.itemDrops.Remove(item);

				garbageUsed = true;
				Destroy(this);
			}
			else
			{
				if (!garbageOpen) interactionTime /= 2;
				garbageOpen = true;
				UIManager.Instance.ShowPopupText("Inventory full!");
			}

		}
	}

	void FirstItem()
	{
		if (Inventory.Instance.AddItem(manager.firstItem))
		{
			UIManager.Instance.ShowPopupText($"You found a {manager.firstItem.name}");
			UIManager.Instance.ShowDialogText("A bottle... maybe i can collect some water for my plant?");


			if (manager.firstItem.oneTimeLoot) manager.itemDrops.Remove(manager.firstItem);

			garbageUsed = true;
			manager.firstItemUsed = true;
			Destroy(this);
		}
	}
}
