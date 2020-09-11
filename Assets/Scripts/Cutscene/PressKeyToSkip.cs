using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PressKeyToSkip : MonoBehaviour
{

	[SerializeField]
	private Text mainText = null;

	private LTDescr makeLight = null;

	protected void OnEnable()
	{
		MakeLight();
		Invoke(nameof(Disappear), 5);
	}

	private void Disappear ()
	{
		this.gameObject.SetActive(false);
	}

	void MakeLight()
	{
		makeLight = LeanTween.textAlpha(mainText.rectTransform, 0, 1.2f)
		.setOnComplete(() => makeLight = LeanTween.textAlpha(mainText.rectTransform, 255, 1.2f)
		.setOnComplete(() => MakeLight())).setLoopPingPong();
	}
}
