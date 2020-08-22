using System;
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

	protected override void Awake()
	{
		base.Awake();
		LockCursorUp();
	}

	private void Start()
    {
        HealthSys.Health.OnValueChangeToMin += Health_OnValueChangedToMin;
    }

	public void LockCursorUp ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	protected void OnDisable()
	{
		HealthSys.Health.OnValueChangeToMin -= Health_OnValueChangedToMin;
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
		// TODO switch e.From
		GameOver("You have lost your health");
	}

	public void GameOver(string reason)
	{
		PlayerPrefs.SetString("GameOverReason", reason);
		SceneManager.LoadScene("GameOver");
	}
	
	
}
