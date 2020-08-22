using System;
using System.Collections;
using System.Collections.Generic;
using Cyberultimate;
using Cyberultimate.Unity;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlantSystem : MonoSingleton<PlantSystem>
{
    public enum State
    {
        Dying, Growing
    }

    private State _plantState = State.Dying;
    
    public State PlantState { 
        get => _plantState;
        set => SetPlantState(value);
    }
    
    public LockValue<uint> PlantSize { get; set; }= new LockValue<uint>(2, 0, 0);

    public Vector2 startingResourcesRandom = new Vector2(40, 70); //min and max
    public Vector2 resourceUseRandom = new Vector2(0.1f, 0.7f); //min and max
    
    public LockValue<float> Water { get; set; } = new LockValue<float>(100, 0, 50);
    public LockValue<float> Soil { get; set; } = new LockValue<float>(100, 0, 50);
    public LockValue<float> FreshAir { get; set; } = new LockValue<float>(100, 0, 50);
    public LockValue<float> Sunlight { get; set; } = new LockValue<float>(100, 0, 50);

    [SerializeField]
    private GameObject plantModelSmall, plantModelMedium, plantModelLarge;

    [SerializeField] 
    private Material growingMaterial, dyingMaterial;
    
    private float waterUse;
    private float soilUse;
    private float freshAirUse;
    private float sunlightUse;

    public enum PlantResources { Soil, Water, Air, Light }

    protected override void Awake()
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

    }
    
    private void Start()
    {
        PlantSize.SetValue(0);
        PlantState = State.Dying;
    }

    private void Update()
    {
        Water.TakeValue(Time.deltaTime * waterUse);
        Soil.TakeValue(Time.deltaTime * soilUse);
        FreshAir.TakeValue(Time.deltaTime * freshAirUse);
        Sunlight.TakeValue(Time.deltaTime * sunlightUse);

        if (Input.GetKeyDown(KeyCode.Y))
        {
            PlantState = State.Dying;
        } else if (Input.GetKeyDown(KeyCode.U))
        {
            PlantState = State.Growing;
        } else if (Input.GetKeyDown(KeyCode.V))
        {
            PlantSize.GiveValue(1);
        } else if (Input.GetKeyDown(KeyCode.B))
        {
            PlantSize.TakeValue(1);
        }
    }

    private float SetToRandom(Vector2 rnd)
    {
        return Random.Range(rnd.x, rnd.y);
    }

    private void ToggleAllModelsOff()
    {
        plantModelSmall.SetActive(false);
        plantModelMedium.SetActive(false);
        plantModelLarge.SetActive(false);
    }
    
    private GameObject GetCurrentPlantModel(uint? val = null)
    {
        switch (val ?? PlantSize.Value)
        {
            case 0:
                return plantModelSmall;
            case 1:
                return plantModelMedium;
            case 2:
                return plantModelLarge;
        }

        return null;
    }

    private void PlantSize_OnValueChanged(object sender, LockValue<uint>.AnyValueChangedArgs e)
    {
        ToggleAllModelsOff();
        GetCurrentPlantModel(e.LockValue.Value).SetActive(true);
        SetPlantState(_plantState);
    }
    
    private void SetPlantState(State newState)
    {
        _plantState = newState;
        var mr = GetCurrentPlantModel().GetComponent<MeshRenderer>();
        Debug.Log($"Setting {mr.gameObject.name} material to {newState}");
        switch (newState)
        {
            case State.Dying:
                mr.material = dyingMaterial;
                break;
            case State.Growing:
                mr.material = growingMaterial;
                break;
        }
    }

    private float modifier = 7f;
    public void AddResources(PlantResources resource, float amount)
    {
        if (resource == PlantResources.Soil)
        {
            Soil.GiveValue(amount * modifier,"");
        } else if (resource == PlantResources.Water)
        {
            Water.GiveValue(amount * modifier, "");
        } else if (resource == PlantResources.Air)
        {
            FreshAir.GiveValue(amount * modifier, "");
        } else if (resource == PlantResources.Light)
        {
            Sunlight.GiveValue(amount * modifier, "");
        }
    }
}
