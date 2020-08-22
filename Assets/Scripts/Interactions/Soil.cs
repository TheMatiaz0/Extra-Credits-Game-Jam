using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soil : InteractableObject
{
    [SerializeField]
    private ItemScriptableObject soil;

    protected override void OnInteract()
    {
        if (Inventory.Instance.AddItem(soil))
        {
            UIManager.Instance.ShowPopupText("Collected soil");
        }
    }
}
