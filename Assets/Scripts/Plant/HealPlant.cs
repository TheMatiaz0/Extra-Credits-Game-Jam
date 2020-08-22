using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlant : InteractableObject
{
    protected override void OnInteract()
    {
        PlantSystem.Instance.AddResources(Inventory.Instance.soil.Value, PlantSystem.PlantResources.Soil);
        PlantSystem.Instance.AddResources(Inventory.Instance.water.Value, PlantSystem.PlantResources.Water);

        Inventory.Instance.DrainResources();

        
        UIManager.Instance.ShowPopupText("Gave all resources to the plant");
    }
}
