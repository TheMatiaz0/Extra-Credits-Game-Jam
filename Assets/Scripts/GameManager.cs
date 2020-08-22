using System;
using Cyberultimate.Unity;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField]
    private HealthSystem healthSys;

    public HealthSystem HealthSys => healthSys;

    [SerializeField]
    private StaminaSystem staminaSys;

    public StaminaSystem StaminaSys => staminaSys;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
