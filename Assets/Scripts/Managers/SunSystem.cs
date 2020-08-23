using System;
using Cyberultimate;
using Cyberultimate.Unity;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using RenderSettings = UnityEngine.RenderSettings;

public class SunSystem : MonoSingleton<SunSystem>
{
    private Light light;

    private void Start()
    {
        light = GetComponent<Light>();
    }

    private void Update()
    {
        var minutes = TimeManager.Instance.CurrentTime.TotalMinutes;
        var minutesInDay = 60 * 18;
        float intensity = (float)(minutesInDay - (minutes - 6*60)) / (float)minutesInDay;
        light.intensity = intensity;
        RenderSettings.skybox.SetFloat("_Exposure", Mathf.Clamp(intensity + 0.2f, 0, 1));
    }
}
