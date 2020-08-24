using System.Collections.Generic;
using System;
using Cyberultimate.Unity;
using Player;
using UI;
using UnityEngine;

namespace Interactions
{
    public class PlantActions : InteractableObject
    {
        protected override void OnInteract()
        {
            PlantNeedsUI.Instance.Show();
        }
    }
}