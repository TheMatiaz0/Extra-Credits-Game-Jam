using System.Collections;
using System.Collections.Generic;
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

    public float Water { get { return water; } set { if (value >= 0) water = value; else water = 0; } }
    public float Soil { get { return soil; } set { if (value >= 0) soil = value; else soil = 0; } }
    public float FreshAir { get { return freshAir; } set { if (value >= 0) freshAir = value; else freshAir = 0; } }
    public float Sunlight { get { return sunlight; } set { if (value >= 0) sunlight = value; else sunlight = 0; } }

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
        Water -= Time.deltaTime * waterUse;
        Soil -= Time.deltaTime * soilUse;
        FreshAir -= Time.deltaTime * freshAirUse;
        Sunlight -= Time.deltaTime * sunlightUse;
    }

    float SetToRandom(Vector2 rnd)
    {
        return Random.Range(rnd.x, rnd.y);
    }
}
