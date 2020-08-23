using Cyberultimate;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class FilmoqueTyping
{
	public static async Task SlowlyTypeUnscaled(string text, float cooldown, Text displayText)
	{
		foreach (char c in text)
		{
			displayText.text += c;
			AudioManager.Instance.PlaySFX("typing");
			await Async.WaitUnscaled(cooldown);
		}
	}
}
