using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : InteractableObject
{
    public List<ItemScriptableObject> itemDrops;
    public float garbageDropChance = 50;

    private bool garbageUsed = false;

    protected override void OnInteract()
    {
        if (garbageUsed)
            return;

        int garbageRnd = Random.Range(0, 100);
        if (garbageRnd <= garbageDropChance)
        {
            PopupText.Instance.ShowText("You found garbage");
        } else
        {
            int itemRnd = Random.Range(0, itemDrops.Count);
            /*if (Inventory.Instance.AddItem(itemDrops[itemRnd]))
            {
                garbageUsed = true;
                gameObject.RemoveComponent<this>();
            } else 
            {
                PopupText.Instance.ShowText("Inventory full!");
            }*/
        }   
    }
}
