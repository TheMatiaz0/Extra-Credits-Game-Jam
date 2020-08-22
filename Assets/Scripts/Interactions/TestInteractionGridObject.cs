using System.Collections.Generic;
using Cyberultimate.Unity;
using UI;
using UnityEngine;

namespace Interactions
{
    public class TestInteractionGridObject : InteractionGridObject
    {
        [SerializeField]
        private List<InteractionGrid.Interaction> interactions;
        protected override List<InteractionGrid.Interaction> Interactions => interactions;
    }
}