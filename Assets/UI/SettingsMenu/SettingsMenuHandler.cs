using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class SettingsMenuHandler : MonoBehaviour
{
    public UnityAction OnSettingsBackButtonClicked = delegate { };

    private Button backButton;

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        backButton = rootVisualElement.Q<Button>("BackButton");

        backButton.clicked += OnBackButtonClicked;
    }

    void OnDisable()
    {
        backButton.clicked -= OnBackButtonClicked;
    }

    void OnBackButtonClicked()
    {
        OnSettingsBackButtonClicked.Invoke();
    }
}
