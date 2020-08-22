using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlant : InteractableObject
{
    public float maximumNeedsToMakeATask = 60;
    PlantNeeds plant;

    private void Start()
    {
        plant = GetComponent<PlantNeeds>();
    }

    protected override void OnInteract()
    {
        TaskManager task = TaskManager.instance;
        if (plant.Water <= maximumNeedsToMakeATask)
        {
            //add task needs water
            Debug.Log("Needs water!");
            task.AddTask("Get water for plant!");
        }
        if (plant.Soil <= maximumNeedsToMakeATask)
        {
            //add task needs soil
            Debug.Log("Needs soil!");
            task.AddTask("Get soil for plant!");
        }
        if (plant.FreshAir <= maximumNeedsToMakeATask)
        {
            //add task needs air
            Debug.Log("Needs air!");
            task.AddTask("Give the plant fresh air!");
        }
        if (plant.Sunlight <= maximumNeedsToMakeATask)
        {
            //add task needs sun
            Debug.Log("Needs sun!");
            task.AddTask("Give the plant some sunlight!");
        }
    }
}
