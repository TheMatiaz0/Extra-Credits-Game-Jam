using UnityEngine;

namespace Interactions
{
    public class TestInteractable : InteractableObject
    {
        protected override void OnInteract()
        {
            Debug.Log("OK!");
        }
    }
}