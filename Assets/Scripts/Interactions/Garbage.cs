using Cyberultimate;
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

	private void Start()
	{
		interactionTime = 3f;
	}

	public override void KeyDown()
	{
		AudioManager.Instance.PlaySFX(GarbageManager.Instance.GarbageSet ? "garbage1" : "garbage2");
	}

	protected override void OnInteract()
	{
		GarbageManager.Instance.GarbageSet = !GarbageManager.Instance.GarbageSet;

		ItemScriptableObject itemScriptable = GarbageManager.Instance.GetRandomItem();

		if (itemScriptable.name == "Garbage")
		{
			UIManager.Instance.ShowPopupText("You found only garbage");
		}

		else
		{
			if (Inventory.Instance.AddItem(itemScriptable))
			{
				UIManager.Instance.ShowPopupText($"You found a {itemScriptable.name}");
			}

			else
			{
				UIManager.Instance.ShowPopupText("Inventory full!");
			}
		}

		Destroy(this);
	}
}
