using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberultimate.Unity;
using Random = UnityEngine.Random;
using System.Linq;

public class Garbage : InteractableObject
{
    public override string InteractionName => "Search garbage";
    public GarbageManager manager;

    [HideInInspector]
    public bool garbageUsed = false;

    private bool garbageOpen = false;
    public float garbageDropChance=50;

    private void Start()
    {
        interactionTime = 3f;
    }

    public override void KeyDown()
    {
        AudioManager.Instance.PlaySFX(manager.garbage1 ? "garbage1" : "garbage2");
    }

    protected override void OnInteract()
    {
        if (garbageUsed) return;
        if (!manager.firstItemUsed) { FirstItem(); return; }

        manager.garbage1 = !manager.garbage1;

        int garbageRnd = Random.Range(0, 100);
        if (garbageRnd <= garbageDropChance)
        {
            UIManager.Instance.ShowPopupText("You found only garbage");
            garbageUsed = true;
            Destroy(this);
        } else
        {
            int m = 0;
            foreach (int i in manager.itemChanceDrops.Values)
            {
                
                if (manager.itemDrops.Contains(manager.itemChanceDrops[manager.itemChanceDrops.Single(s => s.Value == i).Key]))
                {
                    m += i;
                }
            }

            int itemRnd = Random.Range(0, m);

            int currIndex = 0;
            while (itemRnd > 0)
            {
                if (manager.itemChanceDrops[manager.itemDrops[currIndex]] >= itemRnd)
                {
                    break;
                }
                else itemRnd -= manager.itemChanceDrops[manager.itemDrops[currIndex]];
                currIndex += 1;
            }

            var item = manager.itemDrops[currIndex];
            if (Inventory.Instance.AddItem(item))
            {
                UIManager.Instance.ShowPopupText($"You found a {item.name}")

                if (manager.shovelEvolution.Contains(item))
                {
                    int i = manager.shovelEvolution.IndexOf(item);
                    if (manager.shovelEvolution.Count >= i + 2)
                    {
                        manager.itemDrops.Add(manager.shovelEvolution[i + 1]);
                    }
                }
                else if (manager.bottleEvolution.Contains(item))
                {
                    int i = manager.bottleEvolution.IndexOf(item);
                    if (manager.bottleEvolution.Count >= i + 2)
                    {
                        manager.itemDrops.Add(manager.bottleEvolution[i + 1]);
                    }
                }

                if (item.oneTimeLoot) manager.itemDrops.Remove(item);

                garbageUsed = true;
                //Destroy(this);
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
        if (Inventory.Instance.AddItem(manager.firstItem))
        {
            UIManager.Instance.ShowPopupText($"You found a {item.name}")
            UIManager.Instance.ShowDialogText("A bottle... maybe i can collect some water for my plant?");

           
            if (manager.firstItem.oneTimeLoot) manager.itemDrops.Remove(manager.firstItem);

            garbageUsed = true;
            interactionEnabled = false;
            manager.firstItemUsed = true;
            //Destroy(this);
        }
    }
}
