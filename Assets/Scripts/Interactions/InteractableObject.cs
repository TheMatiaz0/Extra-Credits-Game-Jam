using System;
using UI;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractableObject : MonoBehaviour
{
    public float interactionTime = 0;
    public float takesStamina = 0;

    private float holdingTime = 0;

    private bool usable = true;

    public void MouseDown()
    {
        if (interactionTime == 0)
        {
            OnInteract();
        } 
    }

    public void MouseHold()
    {
        if (!usable || interactionTime == 0) return;
        
        if (holdingTime >= interactionTime)
        {
            OnInteract();
            holdingTime = 0;
            usable = false;
        }

        var progress = holdingTime / interactionTime;

        StaminaSystem.Instance.Stamina.Take((uint)Mathf.RoundToInt((takesStamina / interactionTime) * Time.deltaTime * 100), "Interaction");
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
