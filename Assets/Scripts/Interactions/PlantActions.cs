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
        private Animator anim;
        
        public int maximumNeedsToCompleteTask = 60;

        private void Start()
        {
            anim = GetComponent<Animator>();            
        }

        public void CheckPlant()
        {
            var plant = PlantSystem.Instance;
            var task = TaskManager.Instance;

            if (plant.Water.Value <= maximumNeedsToCompleteTask) task.AddTask("Get water for plant"); else task.RemoveTask("Get water for plant");
            if (plant.Soil.Value <= maximumNeedsToCompleteTask) task.AddTask("Get soil for plant"); else task.RemoveTask("Get soil for plant");
            if (plant.Sunlight.Value <= maximumNeedsToCompleteTask) task.AddTask("Give the plant some sunlight"); else task.RemoveTask("Give the plant some sunlight");
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