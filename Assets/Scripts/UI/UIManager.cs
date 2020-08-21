using Cyberultimate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private Image hpBar = null;

	[SerializeField]
	private Image staminaBar = null;

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
}
