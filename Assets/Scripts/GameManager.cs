﻿using System;
using Cyberultimate;
using Cyberultimate.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        HealthSys.Health.OnValueChangeToMin += Health_OnValueChangedToMin;
    }

	protected void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
            SceneManager.LoadScene("Game");
		}
	}

	private void Health_OnValueChangedToMin(object sender, LockValue<float>.AnyValueChangedArgs e)
	{
		SceneManager.LoadScene("GameOver");
	}
	
	
}
