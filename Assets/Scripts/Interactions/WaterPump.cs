using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPump : InteractableObject
{
    public override string InteractionName => "Collect water";
    
    public ItemScriptableObject bottleWithFilter;
    public ItemScriptableObject bigBottleWithFilter;

    public uint waterAmount = 30;

    public override void KeyDown()
    {
        base.KeyDown();
        AudioManager.Instance.PlaySFX("pump1");
    }

    protected override void OnInteract()
    {
        if (bottleWithFilter && Inventory.Instance.HasItem(bigBottleWithFilter.name)) interactionTime -= bigBottleWithFilter.useTakeLessTime;
        else if (bigBottleWithFilter && Inventory.Instance.HasItem(bottleWithFilter.name)) interactionTime -= bottleWithFilter.useTakeLessTime;
        Inventory.Instance.AddResource(waterAmount, PlantSystem.PlantResources.Water);
    }
}
