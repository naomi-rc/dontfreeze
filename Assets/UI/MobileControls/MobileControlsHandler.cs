using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MobileControlsHandler : MonoBehaviour
{
    [SerializeField]
    private InteractionManager interactionManager;

    private Button interactButton;

    private void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        rootVisualElement.BringToFront();

        interactButton = rootVisualElement.Q<Button>("InteractButton");

        interactionManager.OnInteractableChanged += InteractableChanged;
    }

    private void OnDisable()
    {
        interactionManager.OnInteractableChanged -= InteractableChanged;
    }

    private void InteractableChanged(Interactable interactable)
    {
        DisplayStyle style = interactable.type != InteractionType.None ? DisplayStyle.Flex : DisplayStyle.None;
        interactButton.style.display = style;

        switch (interactable.type)
        {
            case InteractionType.PickUp:
                interactButton.text = "Pick Up";
                break;
            case InteractionType.None:
            default:
                break;
        }
    }
}
