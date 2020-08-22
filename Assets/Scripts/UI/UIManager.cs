using Cyberultimate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cyberultimate.Unity;
using System.Globalization;
using System;

public class UIManager : MonoSingleton<UIManager>
{
	[SerializeField]
	private Image hpBar = null;

	[SerializeField]
	private Image staminaBar = null;

	[SerializeField]
	private Text timeText = null;
	[SerializeField]
	private Text dayText = null;

    [SerializeField]
    private Text popupText = null;

	protected void OnEnable()
	{
		HealthSystem.Instance.Health.OnValueChanged += Health_OnValueChanged;
		StaminaSystem.Instance.Stamina.OnValueChanged += Stamina_OnValueChanged;

	}

	private void Instance_OnCurrentTimeChange(object sender, SimpleArgs<TimeSpan> e)
	{
		var dateTime = new DateTime(e.Value.Ticks);
		var formattedTime = dateTime.ToString("h:mm tt", CultureInfo.InvariantCulture);
		timeText.text = formattedTime;
	}

	private void Instance_OnCurrentDayChange(object sender, SimpleArgs<Cint> e)
	{
		dayText.text = $"Day {e.Value}";
	}


	private void Stamina_OnValueChanged(object sender, LockValue<float>.AnyValueChangedArgs e)
	{
		staminaBar.fillAmount = Percent.FromValueInRange(e.Hp.Value, (0, e.Hp.Max)).AsFloat;
	}

	private void Health_OnValueChanged(object sender, LockValue<float>.AnyValueChangedArgs e)
	{
		hpBar.fillAmount = Percent.FromValueInRange(e.Hp.Value, (0, e.Hp.Max)).AsFloat;
	}

	protected void OnDisable()
	{
		HealthSystem.Instance.Health.OnValueChanged -= Health_OnValueChanged;
		StaminaSystem.Instance.Stamina.OnValueChanged -= Stamina_OnValueChanged;

		TimeManager.Instance.OnCurrentDayChange -= Instance_OnCurrentDayChange;
		TimeManager.Instance.OnCurrentTimeChange -= Instance_OnCurrentTimeChange;
	}

	protected void Start()
	{
		TimeManager.Instance.OnCurrentDayChange += Instance_OnCurrentDayChange;
		TimeManager.Instance.OnCurrentTimeChange += Instance_OnCurrentTimeChange;

		hpBar.fillAmount = 1;
		staminaBar.fillAmount = 1;
	}

    public void ShowPopupText(string txt)
    {
        popupText.text = txt;
        popupText.color = new Color(popupText.color.r, popupText.color.b, popupText.color.g, 1);
        LeanTween.alpha(popupText.rectTransform, 1, 1).setOnComplete(() => LeanTween.alpha(popupText.rectTransform,0,0.5f));
        popupText.color = new Color(popupText.color.r, popupText.color.b, popupText.color.g, 0);
    }
}
