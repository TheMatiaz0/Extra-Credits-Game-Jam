﻿using System;
using Cyberultimate.Unity;
using UI;
using UnityEngine;

public class InteractionChecker : MonoSingleton<InteractionChecker>
{
    public float distance = 10;
    public LayerMask layerMask;

    private InteractableObject lastObject;
    
    private void Update()
    {
        var fwd = Camera.main.transform.forward;
        if (Physics.Raycast(transform.position, fwd, out var hit, distance, layerMask))
        {
            Debug.DrawRay(transform.position, fwd, Color.red);
            var go = hit.collider.gameObject;
            if (go.TryGetComponent<InteractableObject>(out var c))
            {
                lastObject = c;
                InteractionUI.Instance.ShowPossibleInteraction();
                if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
                {
                    c.KeyDown();
                } else if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.E))
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
