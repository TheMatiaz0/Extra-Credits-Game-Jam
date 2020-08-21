using System;
using Cyberultimate.Unity;
using UI;
using UnityEngine;

public class InteractionChecker : MonoSingleton<InteractionChecker>
{
    public float distance = 10;

    private InteractableObject lastObject;
    
    private void Update()
    {
        var fwd = Camera.main.transform.forward;
        if (Physics.Raycast(transform.position, fwd, out var hit, distance))
        {
            Debug.DrawRay(transform.position, fwd, Color.red);
            var go = hit.collider.gameObject;
            if (go.TryGetComponent<InteractableObject>(out var c))
            {
                lastObject = c;
                InteractionUI.Instance.ShowPossibleInteraction();
                if (Input.GetMouseButtonDown(0))
                {
                    c.MouseDown();
                } else if (Input.GetMouseButton(0))
                {
                    c.MouseHold();
                }
                else
                {
                    c.MouseUp();
                }
            }
            else
            {
                InteractionUI.Instance.HidePossibleInteraction();
                lastObject?.MouseUp();
            }
        }
        else
        {
            InteractionUI.Instance.HidePossibleInteraction();
            lastObject?.MouseUp();
        }

    }
}
