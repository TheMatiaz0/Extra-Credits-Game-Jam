using System;
using Cyberultimate.Unity;
using UI;
using UnityEngine;

namespace Player
{
    public class Hand : MonoSingleton<Hand>
    {
        private bool isHolding = false;
        private Transform currentylHolding;
        private RigidbodyConstraints previousConstraints;
        
        public void PickUp(Transform target)
        {
            if (isHolding)
            {
                UIManager.Instance.ShowPopupText("Cannot pick up more items");
                return;
            }
            
            InteractionChecker.Instance.checkInteractions = false;
            InteractionUI.Instance.ShowPossibleInteraction();

            isHolding = true;
            target.parent = transform;
            target.localPosition = Vector3.zero;
            currentylHolding = target;
            
            if (target.TryGetComponent<Rigidbody>(out var rb))
            {
                previousConstraints = rb.constraints;
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        public void PutDown()
        {
            if (!isHolding) return;
            
            InteractionChecker.Instance.checkInteractions = true;
            
            currentylHolding.parent = null;
            
            if (currentylHolding.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.useGravity = true;
                rb.constraints = previousConstraints;
            }

            isHolding = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PutDown();
            }
        }
    }
}