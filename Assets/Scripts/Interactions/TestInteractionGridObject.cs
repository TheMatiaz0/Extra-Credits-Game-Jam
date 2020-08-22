using System.Collections.Generic;
using Cyberultimate.Unity;
using UI;
using UnityEngine;

namespace Interactions
{
    public class TestInteractionGridObject : InteractionGridObject
    {
        [SerializeField]
        private ReorderableArray<InteractionGrid.Interaction> interactions;
        protected override IList<InteractionGrid.Interaction> Interactions => interactions;
    }f
}