using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPump : InteractableObject
{
    public override string InteractionName => "Collect water";

    public uint waterAmount = 30;
    private float interactionTimeStateful;

	protected void Awake()
	{
        interactionTimeStateful = interactionTime;
	}

	public override void KeyDown()
    {
        base.KeyDown();
        AudioManager.Instance.PlaySFX("pump1");
        Item it = null;
        if ((it = Inventory.Instance.GetItemCurrentlySelected()) != null)
		{
            interactionTime = interactionTimeStateful;
            interactionTime -= it.UseTakeLessTime;
        }

    }

	protected override void OnInteract()
    {
        Inventory.Instance.AddResource(waterAmount, PlantSystem.PlantResources.Water);
        Inventory.Instance.GetItemCurrentlySelected().Durability.TakeValue(1);
        InventoryUI.Instance.Refresh();
    }
}
