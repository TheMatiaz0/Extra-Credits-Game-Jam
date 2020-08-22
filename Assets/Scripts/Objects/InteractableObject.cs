using System;
using UI;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractableObject : MonoBehaviour
{
    public float interactTime = 0;
    public float takesStamina = 0;
    public string[] itemsNeeded;

    private float holdingTime = 0;

    private bool usable = true;

    public void MouseDown()
    {
        if (interactTime == 0)
        {
            OnInteract();
        } 
    }

    public void MouseHold()
    {
        if (!usable || interactTime == 0) return;
        
        if (holdingTime >= interactTime)
        {
            OnInteract();
            holdingTime = 0;
            usable = false;
        }

        var progress = holdingTime / interactTime;

        StaminaSystem.Instance.Stamina.Take((uint)Mathf.RoundToInt((takesStamina / interactTime) * Time.deltaTime * 100), "Interaction");
        holdingTime += Time.deltaTime;
        InteractionUI.Instance.SetPossibleInteractionProgress(progress);
    }

    public void MouseUp()
    {
        usable = true;
        holdingTime = 0;
    }


    protected abstract void OnInteract();
}
