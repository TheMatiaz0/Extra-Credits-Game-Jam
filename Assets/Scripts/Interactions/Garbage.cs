using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : InteractableObject
{
    public List<ItemScriptableObject> itemDrops;
    public float garbageDropChance = 50;

    private bool garbageUsed = false;
    private bool garbageOpen = false;

    protected override void OnInteract()
    {
        if (garbageUsed)
            return;

        int garbageRnd = Random.Range(0, 100);
        if (garbageRnd <= garbageDropChance)
        {
            UIManager.Instance.ShowPopupText("You found garbage");
            Debug.Log("Garbage");
            garbageUsed = true;
            Destroy(this);
        } else
        {
            int itemRnd = Random.Range(0, itemDrops.Count);
            if (Inventory.Instance.AddItem(itemDrops[itemRnd]))
            {
                garbageUsed = true;
                Destroy(this);
            } else 
            {
                if(!garbageOpen) interactionTime /= 2;
                garbageOpen=true;
                UIManager.Instance.ShowPopupText("Inventory full!");
            }

        }
    }
}
