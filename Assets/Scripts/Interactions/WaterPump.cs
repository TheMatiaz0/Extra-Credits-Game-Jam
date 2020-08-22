using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPump : InteractableObject
{
    [SerializeField]
    private ItemScriptableObject bottleWithWater;

    protected override void OnInteract()
    {
        //Inventory.Instance.RemoveItem(bottle);
        if (Inventory.Instance.AddItem(bottleWithWater))
        {
            UIManager.Instance.ShowPopupText("Got some water");
        }
    }
}
