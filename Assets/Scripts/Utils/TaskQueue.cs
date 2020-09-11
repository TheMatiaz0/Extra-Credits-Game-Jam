
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class TaskQueue<TEl>
{
    public struct Element
    {
        public TEl el;
        public Func<TEl, Task> action;
    }
    
    private Func<TEl, Task> nextAction;

    private Queue<Element> queue = new Queue<Element>();
    private bool inProgress = false;

    public TaskQueue(Func<TEl, Task> action)
    {
        this.nextAction = action;
    }
    
    public void Push(TEl el, Func<TEl, Task> action = null)
    {
        queue.Enqueue(new Element
        {
            el = el,
            action = action
        });
        if (!inProgress) _ = Next();
    }

    private async Task Next()
    {
        inProgress = true;
        var el = queue.Dequeue();
        if (el.action != null) await el.action.Invoke(el.el);
        await nextAction.Invoke(el.el);
        
        inProgress = false;
        if (queue.Count > 0) _ = Next();
    }
}
