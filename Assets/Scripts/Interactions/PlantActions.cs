using System.Collections.Generic;
using System;
using Cyberultimate.Unity;
using Player;
using UI;
using UnityEngine;

namespace Interactions
{
    public class PlantActions : InteractionGridObject
    {
        [SerializeField]
        private ReorderableArray<InteractionGrid.Interaction> interactions;
        protected override IList<InteractionGrid.Interaction> Interactions => interactions;

        public void CheckPlant()
        {
            PlantNeedsUI.Instance.Show();
        }
        
        public void HealPlant()
        {
            PlantSystem.Instance.AddResources(Inventory.Instance.soil.Value, PlantSystem.PlantResources.Soil);
            PlantSystem.Instance.AddResources(Inventory.Instance.water.Value, PlantSystem.PlantResources.Water);

            Inventory.Instance.DrainResources();
            
            UIManager.Instance.ShowPopupText("Gave all resources to the plant");
        }

        public void PickUpPlant()
        {
            Hand.Instance.PickUp(transform);
        }

        private int lightStaminaDrain = 5;
        private int lightTimeSkip = 20;
        public void LightPlant()
        {
            GameManager.Instance.StaminaSys.Stamina.TakeValue(lightStaminaDrain);
            TimeManager.Instance.CurrentTime.Add(TimeSpan.FromMinutes(lightTimeSkip));
        }
    }
}