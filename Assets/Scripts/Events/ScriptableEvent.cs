using TypeReferences;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "ScriptableObjects/Event", order = 0)]
public class ScriptableEvent : ScriptableObject
{
    public string text;
    
    [ClassExtends(typeof(EventLogic))]
    public ClassTypeReference logic;
}