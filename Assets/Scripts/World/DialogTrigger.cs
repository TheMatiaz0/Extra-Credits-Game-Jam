using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DialogTrigger : BaseTrigger
{
    [SerializeField] [TextArea] private string text;
    [SerializeField] private AudioClip voiceline;
    [SerializeField] private float duration = 3f;
    
    protected override void Action()
    {
        Debug.Log("Showing dialog text");
        if (voiceline != null)
        {
            UIManager.Instance.ShowDialogText(text, voiceline);
        }
        else
        {
            UIManager.Instance.ShowDialogText(text, duration);
        }
    }
}
