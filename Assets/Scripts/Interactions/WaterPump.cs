﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPump : InteractableObject
{
    [SerializeField]
    private ItemScriptableObject bottleWithWater;

    protected override void OnInteract()
    {
        Inventory.Instance.AddResource(2,PlantNeeds.PlantResources.water);
    }
}