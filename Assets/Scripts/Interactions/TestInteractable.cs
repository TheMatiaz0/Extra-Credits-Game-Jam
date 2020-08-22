using UnityEngine;

public class TestInteractable : InteractableObject
{
    protected override void OnInteract()
    {
        Debug.Log("interaction!");
    }
}
