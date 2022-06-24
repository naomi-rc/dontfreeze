using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InteractionManager", menuName = "Interactables/InteractionManager")]
public class InteractionManager : ScriptableObject
{
    public UnityAction<Interactable> OnInteractableChanged;

    [SerializeField]
    public InputReader inputReader;

    [SerializeField]
    public InventoryDatabase inventoryDatabase;

    private Interactable currentInteractable = new Interactable(InteractionType.None, null);

    private void OnEnable()
    {
        inputReader.InteractEvent += Interact;
    }

    private void OnDisable()
    {
        inputReader.InteractEvent -= Interact;
    }

    public void AddInteraction(Interactable interactable)
    {
        currentInteractable = interactable;
        if (OnInteractableChanged != null)
        {
            OnInteractableChanged.Invoke(currentInteractable);
        }
    }

    public void RemoveInteraction()
    {
        currentInteractable = new Interactable(InteractionType.None, null);
        if (OnInteractableChanged != null)
        {
            OnInteractableChanged.Invoke(new Interactable(InteractionType.None, null));
        }
    }

    public void Interact()
    {
        if (currentInteractable.type == InteractionType.None)
            return;

        switch (currentInteractable.type)
        {
            case InteractionType.PickUp:
                PickUp();
                break;
            // We can add more interaction types here
            // like open doors or interact with other objects
            case InteractionType.None:
            default:
                break;
        }
    }

    public void PickUp()
    {
        PickableItem pickableItem = currentInteractable.interactableObject.GetComponent<PickableItem>();

        if (pickableItem == null)
            return;

        inventoryDatabase.AddItem(pickableItem.inventoryItem);
        Destroy(currentInteractable.interactableObject);
        RemoveInteraction();
    }
}
