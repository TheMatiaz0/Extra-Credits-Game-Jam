using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Garbage : InteractableObject
{
    public List<ItemScriptableObject> itemDrops;
    public float garbageDropChance = 50;

    public List<ItemScriptableObject> shovelEvolution;
    public List<ItemScriptableObject> bottleEvolution;

    private bool garbageUsed = false;
    private bool garbageOpen = false;

    private void Start()
    {
        interactionTime = 3f;
    }

    protected override void OnInteract()
    {
        if (garbageUsed)
            return;

        int garbageRnd = Random.Range(0, 100);
        if (garbageRnd <= garbageDropChance)
        {
            UIManager.Instance.ShowPopupText("You found only garbage");
            Debug.Log("Garbage");
            garbageUsed = true;
            Destroy(this);
        } else
        {
            int itemRnd = Random.Range(0, itemDrops.Count);
            if (Inventory.Instance.AddItem(itemDrops[itemRnd]))
            {
                UIManager.Instance.ShowPopupText("You found a " + itemDrops[itemRnd].name);

                if (shovelEvolution.Contains(itemDrops[itemRnd]))
                {
                    int i = shovelEvolution.IndexOf(itemDrops[itemRnd]);
                    if (shovelEvolution.Count >= i + 1)
                    {
                        GarbageManager.Instance.AddItemToAll(shovelEvolution[i + 1]);
                    }
                }
                else if (bottleEvolution.Contains(itemDrops[itemRnd]))
                {
                    int i = bottleEvolution.IndexOf(itemDrops[itemRnd]);
                    if (bottleEvolution.Count >= i + 1)
                    {
                        GarbageManager.Instance.AddItemToAll(bottleEvolution[i + 1]);
                    }
                }

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
