using System;
using Cyberultimate;
using Cyberultimate.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameOverType
{
	Died, Failed
}

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

	public void UnlockCursor ()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	protected void OnDisable()
	{
		HealthSys.Health.OnValueChangeToMin -= Health_OnValueChangedToMin;
	}

	private void Health_OnValueChangedToMin(object sender, LockValue<float>.AnyValueChangedArgs e)
	{
		// TODO switch e.From
		GameOver("You have lost your health");
	}

	public void GameOver(string reason, GameOverType type = GameOverType.Died)
	{
		PlayerPrefs.SetString("GameOverType", type.ToString());
		PlayerPrefs.SetString("GameOverReason", reason);
		SceneManager.LoadScene("GameOver");
	}

	public void GameFinishCutscene()
	{
        GameEndingOptions.Instance.StartEnding();
	}
}
