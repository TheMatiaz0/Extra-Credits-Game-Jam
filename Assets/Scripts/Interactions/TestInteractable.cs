using UnityEngine;

namespace Interactions
{
    public class TestInteractable : InteractableObject
    {
        public override string InteractionName => "Test";

        protected override void OnInteract()
        {
            UIManager.Instance.ShowPopupText("OK!");
        }
    }
}