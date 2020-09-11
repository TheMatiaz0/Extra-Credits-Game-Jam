using Cyberultimate;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cyberultimate.Unity;
using System.Globalization;
using System;
using System.Threading.Tasks;

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
	private GameObject goToSleepText = null;

    [SerializeField]
    private Text popupText = null;
    private UITextQueue popupQueue;
    
    [SerializeField]
    private Text dialogText = null;

    private DialogManager dialogs;

	[SerializeField]
	private GameObject resultObj = null;

	protected void Start()
	{
		GameManager.Instance.HealthSys.Health.OnValueChanged += Health_OnValueChanged;
		GameManager.Instance.StaminaSys.Stamina.OnValueChanged += Stamina_OnValueChanged;

		TimeManager.Instance.OnCurrentDayChange += Instance_OnCurrentDayChange;
		TimeManager.Instance.OnCurrentTimeChange += Instance_OnCurrentTimeChange;
		
		popupQueue = new UITextQueue(popupText, 3f, 0.2f, 0.2f);
		dialogs = new DialogManager(new UITextQueue(dialogText, 3f, 0.2f, 0.2f));

		hpBar.fillAmount = 1;
		staminaBar.fillAmount = 1;

		LeanTween.alphaText(dialogText.rectTransform, 0, 0);
		LeanTween.alphaText(popupText.rectTransform, 0, 0);
	}

	public void OpenResults ()
	{
		resultObj.SetActive(true);
	}

	public void CloseResults ()
	{
		resultObj.SetActive(false);
	}

    private void Instance_OnCurrentTimeChange(object sender, SimpleArgs<TimeSpan> e)
	{
		var dateTime = new DateTime(e.Value.Ticks);
		var formattedTime = dateTime.ToString("h:mm tt", CultureInfo.InvariantCulture);

		if (e.Value.Hours >= 20)
		{
			timeText.text = $"<color=red>{formattedTime}</color>";
			goToSleepText.SetActive(true);
			return;
		}
		else
		{
			goToSleepText.SetActive(false);
		}

		timeText.text = formattedTime;
	}

	private void Instance_OnCurrentDayChange(object sender, SimpleArgs<Cint> e)
	{
		dayText.text = $"Day {e.Value}";
	}


	private void Stamina_OnValueChanged(object sender, LockValue<float>.AnyValueChangedArgs e)
	{
		staminaBar.fillAmount = Percent.FromValueInRange(e.LockValue.Value, (0, e.LockValue.Max)).AsFloat;
	}

	private void Health_OnValueChanged(object sender, LockValue<float>.AnyValueChangedArgs e)
	{
		hpBar.fillAmount = Percent.FromValueInRange(e.LockValue.Value, (0, e.LockValue.Max)).AsFloat;
	}

	protected void OnDisable()
	{
		GameManager.Instance.HealthSys.Health.OnValueChanged -= Health_OnValueChanged;
		GameManager.Instance.StaminaSys.Stamina.OnValueChanged -= Stamina_OnValueChanged;

		TimeManager.Instance.OnCurrentDayChange -= Instance_OnCurrentDayChange;
		TimeManager.Instance.OnCurrentTimeChange -= Instance_OnCurrentTimeChange;
	}

	
	public void ShowPopupText(string txt, float? duration = null)
	{
		popupQueue.Push(txt, duration);
	}
	

	public void ShowDialogText(string txt, AudioClip clip)
    {
	    dialogs.Push((txt, clip, 3f));
    }
	
	public void ShowDialogText(string txt, float duration = 3f)
	{
		dialogs.Push((txt, null, duration));
	}
}

