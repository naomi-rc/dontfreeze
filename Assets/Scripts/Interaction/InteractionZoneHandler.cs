using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionZoneHandler : MonoBehaviour
{
    [SerializeField]
    private InteractionManager interactionManager;

    private void OnTriggerEnter(Collider other)
    {
        Interactable potentialInteractable = new Interactable(InteractionType.None, other.gameObject);

        if (other.gameObject.CompareTag("Pickable"))
        {
            potentialInteractable.type = InteractionType.PickUp;
        }

        if (potentialInteractable.type != InteractionType.None)
        {
            interactionManager.AddInteraction(potentialInteractable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactionManager.RemoveInteraction();
    }
}
