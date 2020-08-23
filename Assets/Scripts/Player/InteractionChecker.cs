using System;
using Cyberultimate.Unity;
using UI;
using UnityEngine;

public class InteractionChecker : MonoSingleton<InteractionChecker>
{
    public float distance = 10;
    public LayerMask layerMask;

    private InteractableObject lastObject;

    [HideInInspector] public bool checkInteractions = true;
    
    private void Update()
    {
        if (!checkInteractions)
        {
            lastObject?.ActionUp();
            return;
        }
        
        var fwd = Camera.main.transform.forward;
        //Debug.DrawRay(transform.position, fwd, Color.red);
        if (Physics.Raycast(transform.position, fwd, out var hit, distance, layerMask))
        {
            var go = hit.collider.gameObject;
            if (go.TryGetComponent<InteractableObject>(out var c))
            {
                lastObject = c;
                InteractionUI.Instance.ShowPossibleInteraction();
                if (Input.GetKeyDown(KeyCode.E))
                {
                    c.KeyDown();
                } else if (Input.GetKey(KeyCode.E))
                {
                    c.KeyHold();
                }
                else
                {
                    c.ActionUp();
                }
            }
            else
            {
                InteractionUI.Instance.HidePossibleInteraction();
                lastObject?.ActionUp();
            }
        }
        else
        {
            InteractionUI.Instance.HidePossibleInteraction();
            lastObject?.ActionUp();
        }

    }
}
