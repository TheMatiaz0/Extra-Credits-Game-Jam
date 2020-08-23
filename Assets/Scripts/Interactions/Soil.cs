using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : InteractableObject
{
    public ItemScriptableObject shovel;
    public ItemScriptableObject betterShovel;

    protected override void OnInteract()
    {
        AudioManager.Instance.PlaySFX("shovel");
        if (betterShovel && Inventory.Instance.HasItem(betterShovel.name)) interactionTime -= betterShovel.useTakeLessTime;
        else if (shovel && Inventory.Instance.HasItem(shovel.name)) interactionTime -= shovel.useTakeLessTime;
        Inventory.Instance.AddResource(2, PlantSystem.PlantResources.Soil); //zależnie od rodzaju łopaty
    }
}
