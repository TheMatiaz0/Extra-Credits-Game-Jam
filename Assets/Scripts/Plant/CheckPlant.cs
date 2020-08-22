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
        if (plant.Water <= maximumNeedsToMakeATask)
        {
            //add task needs water
            Debug.Log("Needs water!");
        }
        if (plant.Soil <= maximumNeedsToMakeATask)
        {
            //add task needs soil
            Debug.Log("Needs soil!");
        }
        if (plant.FreshAir <= maximumNeedsToMakeATask)
        {
            //add task needs air
            Debug.Log("Needs air!");
        }
        if (plant.Sunlight <= maximumNeedsToMakeATask)
        {
            //add task needs sun
            Debug.Log("Needs sun!");
        }
    }
}
