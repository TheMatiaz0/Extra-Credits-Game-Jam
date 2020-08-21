using Cyberultimate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	protected void OnEnable()
	{
		HealthSystem.Instance.Health.OnValueChanged += Value_OnValueChanged;
	}

	protected void OnDisable()
	{
		HealthSystem.Instance.Health.OnValueChanged -= Value_OnValueChanged;
	}

	protected void Start()
	{
		hpBar.fillAmount = 1;
		// staminaBar.fillAmount = 1;
	}

	private void Value_OnValueChanged(object sender, LockValue.AnyHpValueChangedArgs e)
	{
		hpBar.fillAmount = Percent.FromValueInRange(e.Hp.Value, (0, e.Hp.Max)).AsFloat;
	}

	[SerializeField]
	private Image hpBar = null;

	[SerializeField]
	private Image staminaBar = null;

	
}
