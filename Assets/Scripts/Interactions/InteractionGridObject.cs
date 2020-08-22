using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Interactions
{
    public abstract class InteractionGridObject : InteractableObject
    {
        protected abstract List<InteractionGrid.Interaction> Interactions { get; }
        
        protected override void OnInteract()
        {
            InteractionGrid.Instance.Show(Interactions);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
            {
                InteractionGrid.Instance.Hide();
            }
        }
    }
}