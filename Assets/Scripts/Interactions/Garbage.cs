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
	public bool GarbageSet { get; set; } = true;

	[SerializeField]
	private GarbageLooter garbageLooter = null;

	public override string InteractionName => "Search garbage";

	private void Start()
	{
		interactionTime = 3f;
	}

	public override void KeyDown()
	{
		AudioManager.Instance.PlaySFX(GarbageSet ? "garbage1" : "garbage2");
	}

	protected override void OnInteract()
	{
		GarbageSet = !GarbageSet;

		ItemScriptableObject itemScriptable = garbageLooter.GetRandomItem();

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
