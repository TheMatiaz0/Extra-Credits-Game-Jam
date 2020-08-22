using System.Collections;
using System.Collections.Generic;
using Cyberultimate;
using Cyberultimate.Unity;
using UnityEngine;

public class PlantNeeds : MonoSingleton<PlantNeeds>
{
    public Vector2 startingResourcesRandom = new Vector2(40, 70); //min and max
    public Vector2 resourceUseRandom = new Vector2(0.1f, 0.7f); //min and max

    [SerializeField]
    private float water;
    [SerializeField]
    private float soil;
    [SerializeField]
    private float freshAir;
    [SerializeField]
    private float sunlight;

    public LockValue<float> Water = new LockValue<float>(100, 0, 50);
    public LockValue<float> Soil = new LockValue<float>(100, 0, 50);
    public LockValue<float> FreshAir = new LockValue<float>(100, 0, 50);
    public LockValue<float> Sunlight = new LockValue<float>(100, 0, 50);

    private float waterUse;
    private float soilUse;
    private float freshAirUse;
    private float sunlightUse;

    private void Start()
    {
        water = SetToRandom(startingResourcesRandom);
        soil = SetToRandom(startingResourcesRandom);
        freshAir = SetToRandom(startingResourcesRandom);
        sunlight = SetToRandom(startingResourcesRandom);

        waterUse = SetToRandom(resourceUseRandom);
        soilUse = SetToRandom(resourceUseRandom);
        freshAirUse = SetToRandom(resourceUseRandom);
        sunlightUse = SetToRandom(resourceUseRandom);
    }

    private void Update()
    {
        Water.TakeValue(Time.deltaTime * waterUse);
        Soil.TakeValue(Time.deltaTime * soilUse);
        FreshAir.TakeValue(Time.deltaTime * freshAirUse);
        Sunlight.TakeValue(Time.deltaTime * sunlightUse);
    }

    float SetToRandom(Vector2 rnd)
    {
        return Random.Range(rnd.x, rnd.y);
    }
}
