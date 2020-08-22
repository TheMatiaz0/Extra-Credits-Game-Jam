using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlant : InteractableObject
{
    protected override void OnInteract()
    {
        Inventory.Instance.DrainResources(Inventory.Instance.water.Value,PlantSystem.PlantResources.water);
        Inventory.Instance.DrainResources(Inventory.Instance.soil.Value, PlantSystem.PlantResources.soil);
        UIManager.Instance.ShowPopupText("Gave all resources to the plant");
    }
}
