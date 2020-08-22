using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPump : InteractableObject
{
    [SerializeField]
    private ItemScriptableObject bottle;

    [SerializeField]
    private ItemScriptableObject bottleWithWater;

    protected override void OnInteract()
    {
        //Inventory.Instance.RemoveItem(bottle);
        Inventory.Instance.AddItem(bottleWithWater);
    }
}
