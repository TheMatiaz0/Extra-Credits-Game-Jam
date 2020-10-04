using Cyberultimate.Unity;
using System;
using UnityEngine;

public class TownMusic : MonoSingleton<TownMusic>
{
	[SerializeField] private AudioSource source;

	public void Silence()
	{
		FadeVolume(source.volume, 0);
	}

	public void RestoreVolume()
	{
		FadeVolume(source.volume, 1);
	}

	private void FadeVolume(float from, float to, Action callback = null)
	{
		LeanTween.value(source.gameObject,
				(v) =>
				{
					source.volume = v;
				}, from, to, (float)(0.5f)).setIgnoreTimeScale(true)
			.setOnComplete(_ =>
			{
				callback?.Invoke();
			});
	}
}
