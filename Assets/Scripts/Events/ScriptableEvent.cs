using TypeReferences;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "ScriptableObjects/Event", order = 0)]
public class ScriptableEvent : ScriptableObject
{
   
    [ClassExtends(typeof(EventLogic))]
    public ClassTypeReference logic;
}