using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerTask : BaseTrigger
{
    public enum ActionType
    {
        Add, Remove
    }

    [SerializeField] private ActionType type;
    [SerializeField] private string taskText;
    
    protected override void Action()
    {
        switch (type)
        {
            case ActionType.Add:
                TaskManager.Instance.AddTask(taskText);
                break;
            case ActionType.Remove:
                TaskManager.Instance.RemoveTask(taskText);
                break;
        }
    }
}
