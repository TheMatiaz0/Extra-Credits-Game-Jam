
using System.Threading.Tasks;
using Cyberultimate;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager
{
    private TaskQueue<(string text, AudioClip clip, float duration)> queue;
    private UITextQueue textQueue;

    public DialogManager(UITextQueue textQueue)
    {
        queue = new TaskQueue<(string text, AudioClip clip, float duration)>(Next);
        this.textQueue = textQueue;
    }

    public void Push((string text, AudioClip clip, float duration) el)
    {
        queue.Push(el);
    }

    private async Task Next((string text, AudioClip clip, float duration) el)
    {
        
        textQueue.Push(el.text, el.clip?.length ?? el.duration, async (x) =>
        {
            if(el.clip != null) AudioManager.Instance.PlayClip(el.clip);
        });
    }
}
