using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cyberultimate;
using UnityEngine;
using UnityEngine.UI;

public class UITextQueue
{
    private Text text;

    private TaskQueue<(string message, float duration)> queue;
    
    private readonly float defaultDuration;
    private float inDuration, outDuration;

    public UITextQueue(Text text, float defaultDuration, float inDuration, float outDuration)
    {
        this.text = text;
        this.defaultDuration = defaultDuration;
        this.inDuration = inDuration;
        this.outDuration = outDuration;
        
        queue = new TaskQueue<(string message, float duration)>(NextAction);
    }

    public void Push(string message, float? duration = null, Func<(string message, float duration), Task> action = null)
    {
        queue.Push((message, duration ?? defaultDuration), action);
    }

    private async Task NextAction((string message, float duration) el)
    {
        text.text = el.message;
        LeanTween.textAlpha(text.rectTransform, 1, inDuration);
        await Async.Wait(TimeSpan.FromSeconds(el.duration));
        LeanTween.textAlpha(text.rectTransform, 0, outDuration);
    }
}
