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
    public bool canUseWithoutStamina = false;
    public ReorderableArray<ReorderableArray<ItemScriptableObject>> itemsNeeded;
    
    public abstract string InteractionName { get; }

    private float holdingTime = 0;

    [HideInInspector]
    public bool interactionEnabled=true;

    private bool usable = true;

    private bool CheckItemsNeeded(bool showMessage = false)
    {
        if (GameManager.Instance.StaminaSys.Stamina.Value == 0 && !canUseWithoutStamina)
        {
            UIManager.Instance.ShowPopupText("You are too tired");
            return false;
        }

        var missingItems = new List<string>();
        foreach (var itemOptions in itemsNeeded)
        {
            if (!itemOptions.Any(x => Inventory.Instance.HasItem(x.name)))
            {
                missingItems.Add(string.IsNullOrEmpty(itemOptions[0].tag) ? itemOptions[0].name : itemOptions[0].tag);
            }
        }

        if (missingItems.Count <= 0) return true;

        if (showMessage)
        {
            UIManager.Instance.ShowPopupText($"Required items: {string.Join("; ", missingItems)}");
            missingItems.ForEach(x => TaskManager.Instance.AddTask($"Find {x}"));
        }
        
        return false;
    }
    
    public virtual void KeyDown()
    {
        if (!CheckItemsNeeded(true)) return;
        if (interactionTime == 0)
        {
            OnInteract();
        } 
    }

    public virtual void KeyHold()
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

        GameManager.Instance.StaminaSys.Stamina.TakeValue((takesStamina / interactionTime) * Time.deltaTime, "Interaction");
        holdingTime += Time.deltaTime;
        InteractionUI.Instance.SetPossibleInteractionProgress(progress);
    }

    public virtual void ActionUp()
    {
        usable = true;
        holdingTime = 0;
    }


    protected abstract void OnInteract();
}
