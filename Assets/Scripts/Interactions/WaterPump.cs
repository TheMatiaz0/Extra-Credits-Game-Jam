using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPump : InteractableObject
{
    public ItemScriptableObject bottleWithFilter;
    public ItemScriptableObject bigBottleWithFilter;

    protected override void OnInteract()
    {
        if (bottleWithFilter && Inventory.Instance.HasItem(bigBottleWithFilter.name)) interactionTime -= bigBottleWithFilter.useTakeLessTime;
        else if (bigBottleWithFilter && Inventory.Instance.HasItem(bottleWithFilter.name)) interactionTime -= bottleWithFilter.useTakeLessTime;
        Inventory.Instance.AddResource(2, PlantSystem.PlantResources.Water);
    }
}
