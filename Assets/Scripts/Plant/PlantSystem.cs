﻿using Cyberultimate;
using Cyberultimate.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlantSystem : MonoSingleton<PlantSystem>
{
	public enum State
	{
		Dying, Growing
	}

	private State _plantState = State.Dying;

	private int maximumNeedsToCompleteATask;

	public State PlantState
	{
		get => (Water.Value + Soil.Value + Sunlight.Value > maximumNeedsToCompleteATask * 3) ? State.Growing : State.Dying;
		set => SetPlantState(value);
	}

	public LockValue<uint> PlantSize { get; set; } = new LockValue<uint>(2, 0, 0);

	public Vector2 startingResourcesRandom = new Vector2(40, 70); //min and max
	public Vector2 resourceUseRandom = new Vector2(0.1f, 0.7f); //min and max

	public LockValue<float> Water { get; set; } = new LockValue<float>(100, 0, 50);
	public LockValue<float> Soil { get; set; } = new LockValue<float>(100, 0, 50);
	public LockValue<float> Sunlight { get; set; } = new LockValue<float>(100, 0, 50);

	public float sunlightGain = 0.4f;

    private float hardnessMultiplier;

	public List<int> hoursPlantNeedsChange;

	private int failedDays;

	private Transform sunlight;
	[SerializeField] private LayerMask layerMask;

	[SerializeField]
	private GameObject plantModelSmall, plantModelMedium, plantModelLarge;

	[SerializeField]
	private Material growingMaterial, dyingMaterial;


	private float waterUse;
	private float soilUse;
	private float sunlightUse;

	private uint daysGrowing = 0;

	public enum PlantResources { Soil, Water, Light }

	protected override void Awake()
	{
		base.Awake();

		PlantSize.OnValueChanged += PlantSize_OnValueChanged;

	}

	[SerializeField]
	private Interactions.PlantActions plantActions;

	private void Start()
	{
		sunlight = SunSystem.Instance.transform;

		maximumNeedsToCompleteATask = PlantNeedsUI.Instance.maximumNeedsToCompleteTask;
		PlantSize.SetValue(0);
		PlantState = State.Dying;
		TimeManager.Instance.OnCurrentDayChange += OnDayChange;
		TimeManager.Instance.OnCurrentTimeChange += OnTimeChange;

		ResetResources();
	}

	private void OnDisable()
	{
		TimeManager.Instance.OnCurrentDayChange -= OnDayChange;
		TimeManager.Instance.OnCurrentTimeChange -= OnTimeChange;
	}

	public Color GetColorBasedOnState()
	{
		switch (PlantState)
		{
			case State.Dying:
				return Color.red;

			default:
				return Color.green;
		}
	}

    private bool lastStateInside = true;
    private void Update()
    {
        Soil.TakeValue(Time.deltaTime * (soilUse * (1.2f + hardnessMultiplier)));

        //Debug.DrawRay(transform.position + (transform.up * 1.5f), (sunlight.eulerAngles), Color.red);
        if (Physics.Raycast(transform.position + (transform.up * 1.5f), (sunlight.eulerAngles),50f, layerMask)) // if plant is inside
        {
            if (!lastStateInside)
            {
                PlantParticles.Instance.ChangeSun(false);
            }

            Sunlight.TakeValue(Time.deltaTime * (sunlightUse * (1.45f + hardnessMultiplier)));
            Water.TakeValue(Time.deltaTime * (waterUse * (0.95f + hardnessMultiplier)));
            //Debug.Log("plant inside");

            lastStateInside = true;
        }
        else
        {
            if(lastStateInside)
            {
                PlantParticles.Instance.ChangeSun(true);
            }

            Water.TakeValue(Time.deltaTime * waterUse * (2.1f + hardnessMultiplier));
            Sunlight.GiveValue(Time.deltaTime * (sunlightGain + 0.8f));
            
            lastStateInside = false;

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

    
    //private float modifier = 7f;
    public void AddResources(float amount, PlantResources resource)
    {
        if (resource != PlantResources.Light)
        {
            PlantParticles.Instance.WowParticles();
            if (resource == PlantResources.Soil)
            {
                Soil.GiveValue(amount, "");

            }
            else if (resource == PlantResources.Water)
            {
                Water.GiveValue(amount, "");
                Debug.Log("amount: " + amount + " now: " + Water.Value);
            }
        } else Sunlight.GiveValue(amount, "");
    }

    private void ResetResources()
    {
        waterUse = SetToRandom(resourceUseRandom);
        soilUse = SetToRandom(resourceUseRandom);
        sunlightUse = SetToRandom(resourceUseRandom);

        Water.SetValue(SetToRandom(startingResourcesRandom));
        Soil.SetValue(SetToRandom(startingResourcesRandom));
        Sunlight.SetValue(SetToRandom(startingResourcesRandom));
    }

    private void OnDayChange(object sender, SimpleArgs<Cint> e)
    {
        if (e.Value <= 1) return;
        SetPlantState(PlantState);
        if (PlantState == State.Growing)
        {
            daysGrowing++;
            if (daysGrowing >= 5)
            {
                PlantSize.GiveValue(1);
                daysGrowing = 0;
                HomeMusic.Instance.NextTrack();
            }
        }
        else
        {
            daysGrowing = 0;
            failedDays++;
            if (failedDays >= 4)
            {
                GameManager.Instance.GameOver("The plant died!", GameOverType.Failed);
            }
        }

        hardnessMultiplier += 0.025f;

        Debug.Log($"Day finished. plantState: {PlantState.ToString()}, daysGrowing: {daysGrowing}, failedDays: {failedDays}");
        
        // ResetResources();
    }

    private bool cutsceneShown = false;
    private void OnTimeChange(object sender, SimpleArgs<TimeSpan> args)
    {
        if (PlantSize.Value == 2 && /* args.Value >= TimeSpan.FromHours(19) && */ !cutsceneShown)
        {
            cutsceneShown = true;
            GameManager.Instance.GameFinishCutscene();
            Debug.Log("Game finish cutscene!");
        }
    }

    public void ChangeResources(int hours)
    {
        if (hoursPlantNeedsChange.Contains(hours))
        {
            waterUse = SetToRandom(resourceUseRandom);
            soilUse = SetToRandom(resourceUseRandom);
            sunlightUse = SetToRandom(resourceUseRandom);
        }
    }
}
