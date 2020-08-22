using Cyberultimate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cyberultimate.Unity;

public class UIManager : MonoSingleton<UIManager>
{
	[SerializeField]
	private Image hpBar = null;

	[SerializeField]
	private Image staminaBar = null;

	[SerializeField]
	private Text timeText = null;

    [SerializeField]
    private Animator popupAnimation = null;
    [SerializeField]
    private Text popupText = null;

	protected void OnEnable()
	{
		HealthSystem.Instance.Health.OnValueChanged += Health_OnValueChanged;
		StaminaSystem.Instance.Stamina.OnValueChanged += Stamina_OnValueChanged;
	}

	private void Stamina_OnValueChanged(object sender, LockValue.AnyHpValueChangedArgs e)
	{
		staminaBar.fillAmount = Percent.FromValueInRange(e.LockedValue.Value, (0, e.LockedValue.Max)).AsFloat;
	}

	private void Health_OnValueChanged(object sender, LockValue.AnyHpValueChangedArgs e)
	{
		hpBar.fillAmount = Percent.FromValueInRange(e.LockedValue.Value, (0, e.LockedValue.Max)).AsFloat;
	}

	protected void OnDisable()
	{
		HealthSystem.Instance.Health.OnValueChanged -= Health_OnValueChanged;
		StaminaSystem.Instance.Stamina.OnValueChanged -= Stamina_OnValueChanged;
	}

	protected void Start()
	{
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
