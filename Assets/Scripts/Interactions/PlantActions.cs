using System.Collections.Generic;
using Cyberultimate.Unity;
using UI;
using UnityEngine;

namespace Interactions
{
    public class PlantActions : InteractionGridObject
    {
        [SerializeField]
        private ReorderableArray<InteractionGrid.Interaction> interactions;
        protected override IList<InteractionGrid.Interaction> Interactions => interactions;
        
        public float maximumNeedsToMakeATask = 60;
        public void CheckPlant()
        {
            var plant = PlantSystem.Instance;
            var task = TaskManager.Instance;
            if (plant.Water.Value <= maximumNeedsToMakeATask)
            {
                task.AddTask("Get water for plant");
            }
            if (plant.Soil.Value <= maximumNeedsToMakeATask)
            {
                task.AddTask("Get soil for plant");
            }
            if (plant.FreshAir.Value <= maximumNeedsToMakeATask)
            {
                task.AddTask("Give the plant fresh air");
            }
            if (plant.Sunlight.Value <= maximumNeedsToMakeATask)
            {
                task.AddTask("Give the plant some sunlight");
            }
        }
        
        public void HealPlant()
        {
            PlantSystem.Instance.AddResources(Inventory.Instance.soil.Value, PlantSystem.PlantResources.Soil);
            PlantSystem.Instance.AddResources(Inventory.Instance.water.Value, PlantSystem.PlantResources.Water);

            Inventory.Instance.DrainResources();
            
            UIManager.Instance.ShowPopupText("Gave all resources to the plant");
        }
    }
}