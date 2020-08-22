using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlant : InteractableObject
{
    public float maximumNeedsToMakeATask = 60;

    private PlantSystem plant;
    private void Start()
    {
        plant = PlantSystem.Instance;
    }

    protected override void OnInteract()
    {
        TaskManager task = TaskManager.Instance;
        if (plant.Water.Value <= maximumNeedsToMakeATask)
        {
            //add task needs water
            Debug.Log("Needs water!");
            task.AddTask("Get water for plant");
        }
        if (plant.Soil.Value <= maximumNeedsToMakeATask)
        {
            //add task needs soil
            Debug.Log("Needs soil!");
            task.AddTask("Get soil for plant");
        }
        if (plant.FreshAir.Value <= maximumNeedsToMakeATask)
        {
            //add task needs air
            Debug.Log("Needs air!");
            task.AddTask("Give the plant fresh air");
        }
        if (plant.Sunlight.Value <= maximumNeedsToMakeATask)
        {
            //add task needs sun
            Debug.Log("Needs sun!");
            task.AddTask("Give the plant some sunlight");
        }
    }
}
