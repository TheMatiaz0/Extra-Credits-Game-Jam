using System;
using System.Collections.Generic;
using System.Linq;
using Cyberultimate.Unity;
using UI;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractableObject : MonoBehaviour
{
    public float interactionTime = 0;
    public float takesStamina = 0;
    public ReorderableArray<ReorderableArray<string>> itemsNeeded;

    private float holdingTime = 0;

    private bool usable = true;

    private bool CheckItemsNeeded(bool showMessage = false)
    {
        List<string> missingItems = new List<string>();
        foreach (var itemOptions in itemsNeeded)
        {
            if (!itemOptions.Any(x => Inventory.Instance.HasItem(x)))
            {
                missingItems.Add(string.Join(" or ", itemOptions));
            }
        }

        if (missingItems.Count <= 0) return true;

        if (showMessage)
        {
            UIManager.Instance.ShowPopupText($"Required items: {string.Join("; ", missingItems)}");
        }
        
        return false;
    }
    
    public void KeyDown()
    {
        if (!CheckItemsNeeded(true)) return;
        if (interactionTime == 0)
        {
            OnInteract();
        } 
    }

    public void KeyHold()
    {
        if (!usable || interactionTime == 0) return;
        if (!CheckItemsNeeded()) return;
        
        if (holdingTime >= interactionTime)
        {
            OnInteract();
            holdingTime = 0;
            usable = false;
        }

        var progress = holdingTime / interactionTime;

        StaminaSystem.Instance.Stamina.TakeValue((takesStamina / interactionTime) * Time.deltaTime, "Interaction");
        holdingTime += Time.deltaTime;
        InteractionUI.Instance.SetPossibleInteractionProgress(progress);
    }

    public void ActionUp()
    {
        usable = true;
        holdingTime = 0;
    }


    protected abstract void OnInteract();
}
