using Cyberultimate.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoSingleton<CanvasManager>
{
	private float mainAlpha, cutsceneAlpha;

	[SerializeField]
	private CanvasGroup mainCanvas = null, cutsceneCanvas = null, endingCanvas = null;

	public void ShowMainCanvas ()
	{
		mainCanvas.alpha = 1;
		cutsceneCanvas.alpha = 0;
	}

	public void EndingInteractable (bool isTrue)
	{
		endingCanvas.blocksRaycasts = isTrue;
	}

	public void ShowCutsceneCanvas ()
	{
		mainCanvas.alpha = 0;
		cutsceneCanvas.alpha = 1;
	}

	public void ShowOnlyEndingCanvas ()
	{
		endingCanvas.alpha = 1;
		mainCanvas.alpha = 0;
		cutsceneCanvas.alpha = 0;
	}

	public void HideEverything()
	{
		mainAlpha = mainCanvas.alpha;
		cutsceneAlpha = cutsceneCanvas.alpha;

		mainCanvas.alpha = 0;
		cutsceneCanvas.alpha = 0;
		endingCanvas.alpha = 0;
	}

	public void Restore ()
	{
		mainCanvas.alpha = mainAlpha;
		cutsceneCanvas.alpha = cutsceneAlpha;
	}

	public void Toggle()
	{
		if (mainCanvas.alpha == 0)
		{
			Restore();
		}
		else
		{
			HideEverything();
		}
	}

	
}
