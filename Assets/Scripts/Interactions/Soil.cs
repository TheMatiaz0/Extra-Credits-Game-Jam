using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : InteractableObject
{
    [SerializeField]
    private ItemScriptableObject soil;

    protected override void OnInteract()
    {
        Inventory.Instance.AddResource(2,Inventory.PlantResources.soil); //zależnie od rodzaju łopaty
    }
}
