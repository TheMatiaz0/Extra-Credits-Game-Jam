using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantParticles : MonoBehaviour
{
    [SerializeField]
    private GameObject airPlus;
    [SerializeField]
    private GameObject airMinus;
    [SerializeField]
    private GameObject lightPlus;
    [SerializeField]
    private GameObject lightMinus;

    private void Start()
    {
        airPlus.SetActive(true);
        lightMinus.SetActive(true);

        airMinus.SetActive(false);
        lightPlus.SetActive(false);
    }

    public void ChangeParticles()
    {
        airPlus.SetActive(!airPlus.activeInHierarchy);
        lightPlus.SetActive(!lightPlus.activeInHierarchy);
        airMinus.SetActive(!airMinus.activeInHierarchy);
        lightMinus.SetActive(!lightMinus.activeInHierarchy);
    }

}
