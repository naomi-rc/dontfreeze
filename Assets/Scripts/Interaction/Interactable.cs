using UnityEngine;

public enum InteractionType
{
    None,
    PickUp,
    // Use,
    // Open/Close,
    // Etc...
}

public class Interactable
{
    public InteractionType type;
    public GameObject interactableObject;

    public Interactable(InteractionType type, GameObject interactableObject)
    {
        this.type = type;
        this.interactableObject = interactableObject;
    }
}
