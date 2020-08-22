﻿using System.Collections;
using System.Collections.Generic;
using Cyberultimate;
using Cyberultimate.Unity;
using UnityEngine;

public class PlantSystem : MonoSingleton<PlantSystem>
{
    public enum State
    {
        Dying, Growing
    }

    public State PlantState { get; set; } = State.Dying;
    public LockValue<uint> PlantSize { get; set; }= new LockValue<uint>(2, 0, 0);

    public Vector2 startingResourcesRandom = new Vector2(40, 70); //min and max
    public Vector2 resourceUseRandom = new Vector2(0.1f, 0.7f); //min and max
    
    public LockValue<float> Water { get; set; } = new LockValue<float>(100, 0, 50);
    public LockValue<float> Soil { get; set; } = new LockValue<float>(100, 0, 50);
    public LockValue<float> FreshAir { get; set; } = new LockValue<float>(100, 0, 50);
    public LockValue<float> Sunlight { get; set; } = new LockValue<float>(100, 0, 50);

    [SerializeField]
    private GameObject plantModelSmall, plantModelMedium, plantModelLarge;
    
    private float waterUse;
    private float soilUse;
    private float freshAirUse;
    private float sunlightUse;

    public enum PlantResources { soil, water, air, light }

    private void Start()
    {
        waterUse = SetToRandom(resourceUseRandom);
        soilUse = SetToRandom(resourceUseRandom);
        freshAirUse = SetToRandom(resourceUseRandom);
        sunlightUse = SetToRandom(resourceUseRandom);

        var startResources = SetToRandom(startingResourcesRandom);
        
        Water.SetValue(startResources);
        Soil.SetValue(startResources);
        FreshAir.SetValue(startResources);
        Sunlight.SetValue(startResources);

        PlantSize.OnValueChanged += PlantSize_OnValueChanged;
        PlantSize.SetValue(0);
    }

    private void Update()
    {
        Water.TakeValue(Time.deltaTime * waterUse);
        Soil.TakeValue(Time.deltaTime * soilUse);
        FreshAir.TakeValue(Time.deltaTime * freshAirUse);
        Sunlight.TakeValue(Time.deltaTime * sunlightUse);
    }

    private float SetToRandom(Vector2 rnd)
    {
        return Random.Range(rnd.x, rnd.y);
    }

    private void PlantSize_OnValueChanged(object sender, LockValue<uint>.AnyValueChangedArgs e)
    {
        ToggleAllModelsOff();
        switch (e.LockValue.Value)
        {
            case 0:
                plantModelSmall.SetActive(true);
                break;
            case 1:
                plantModelMedium.SetActive(true);
                break;
            case 2:
                plantModelLarge.SetActive(true);
                break;
        }
    }

    private void ToggleAllModelsOff()
    {
        plantModelSmall.SetActive(false);
        plantModelMedium.SetActive(false);
        plantModelLarge.SetActive(false);
    }

    private float modifier = 7f;
    public void AddResources(PlantResources resource, float amount)
    {
        if (resource == PlantResources.soil)
        {
            Soil.GiveValue(amount*modifier,"");
        } else if (resource == PlantResources.water)
        {
            Water.GiveValue(amount * modifier, "");
        } else if (resource == PlantResources.air)
        {
            FreshAir.GiveValue(amount * modifier, "");
        } else if (resource == PlantResources.light)
        {
            Sunlight.GiveValue(amount * modifier, "");
        }
    }
}
