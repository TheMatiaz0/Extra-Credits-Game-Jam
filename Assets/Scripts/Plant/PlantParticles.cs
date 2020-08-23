using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cyberultimate.Unity;

public class PlantParticles : MonoSingleton<PlantParticles>
{
    [SerializeField]
    private GameObject sun;
    [SerializeField]
    private ParticleSystem wow1;
    [SerializeField]
    private ParticleSystem wow2;
    [SerializeField]
    private ParticleSystem wow3;

    private void Start()
    {
        sun.SetActive(false);
    }

    public void ChangeSun(bool value)
    {
        sun.SetActive(value);
    }

    public void WowParticles()
    {
        wow1.Play();
        wow2.Play();
        wow3.Play();
    }

}
