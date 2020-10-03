using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : InteractableObject
{
    public override string InteractionName => "Dig soil";

    public uint soilAmount = 30;
    private float interactionTimeStateful;

    protected void Awake()
    {
        interactionTimeStateful = interactionTime;
    }

    public override void KeyDown()
    {
        base.KeyDown();
        AudioManager.Instance.PlaySFX("shovel");

        interactionTime = interactionTimeStateful;
        interactionTime -= Inventory.Instance.GetItemCurrentlySelected().UseTakeLessTime;
    }

    protected override void OnInteract()
    {
        Inventory.Instance.AddResource(soilAmount, PlantSystem.PlantResources.Soil); //zależnie od rodzaju łopaty
        Inventory.Instance.GetItemCurrentlySelected().Durability.TakeValue(1);
        InventoryUI.Instance.Refresh();
    }
}
