using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberultimate.Unity;
using Random = UnityEngine.Random;

public class Garbage : InteractableObject
{
    public override string InteractionName => "Search garbage";
    
    public SerializedDictionary<ItemScriptableObject, int> itemChanceDrops = new SerializedDictionary<ItemScriptableObject, int>();
    public List<ItemScriptableObject> itemDrops;
    public float garbageDropChance = 50;

    public List<ItemScriptableObject> shovelEvolution;
    public List<ItemScriptableObject> bottleEvolution;

    public ItemScriptableObject firstItem;

    private bool garbageUsed = false;
    private bool garbageOpen = false;

    public bool firstItemGet = false;

    private bool garbage1 = false;
    
    private void Start()
    {
        interactionTime = 3f;
    }

    public override void KeyDown()
    {
        AudioManager.Instance.PlaySFX(garbage1 ? "garbage1" : "garbage2");
    }

    protected override void OnInteract()
    {
        if (garbageUsed) return;
        if (!firstItemGet) { FirstItem(); return; }
        
        garbage1 = !garbage1;

        int garbageRnd = Random.Range(0, 100);
        if (garbageRnd <= garbageDropChance)
        {
            UIManager.Instance.ShowPopupText("You found only garbage");
            Debug.Log("Garbage");
            garbageUsed = true;
            Destroy(this);
        } else
        {
            int m = 0;
            foreach (int i in itemChanceDrops.Values) m += i;
            int itemRnd = Random.Range(0, m);

            int currIndex = 0;
            while (itemRnd > 0)
            {
                if (itemChanceDrops[itemDrops[currIndex]] >= itemRnd)
                {
                    break;
                }
                else itemRnd -= itemChanceDrops[itemDrops[currIndex]];
                currIndex += 1;
            }

            var item = itemDrops[currIndex];
            if (Inventory.Instance.AddItem(item))
            {
                UIManager.Instance.ShowPopupText("You found a " + item.name);

                if (shovelEvolution.Contains(item))
                {
                    int i = shovelEvolution.IndexOf(item);
                    if (shovelEvolution.Count >= i + 2)
                    {
                        GarbageManager.Instance.AddItemToAll(shovelEvolution[i + 1]);
                    }
                }
                else if (bottleEvolution.Contains(item))
                {
                    int i = bottleEvolution.IndexOf(item);
                    if (bottleEvolution.Count >= i + 2)
                    {
                        GarbageManager.Instance.AddItemToAll(bottleEvolution[i + 1]);
                    }
                }

                if (item.oneTimeLoot) itemDrops.Remove(item);

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

    void FirstItem()
    {
        if (Inventory.Instance.AddItem(firstItem))
        {
            UIManager.Instance.ShowPopupText("You found a " + firstItem.name);
            UIManager.Instance.ShowDialogText("A bottle... maybe i can collect some water for my plant?");

            GarbageManager.Instance.FirstItemUsedToAll();

            if (firstItem.oneTimeLoot) itemDrops.Remove(firstItem);

            garbageUsed = true;
            firstItemGet = true;
            Destroy(this);
        }
    }
}
