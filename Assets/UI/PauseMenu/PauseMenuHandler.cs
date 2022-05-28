using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PauseMenuHandler : MonoBehaviour
{
    public UnityAction ResumeButtonAction = delegate { };
    public UnityAction SettingsButtonAction = delegate { };

    private Button resumeButton;
    private Button settingsButton;
    private Button quitButton;

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        resumeButton = rootVisualElement.Q<Button>("ResumeButton");
        settingsButton = rootVisualElement.Q<Button>("SettingsButton");
        quitButton = rootVisualElement.Q<Button>("QuitButton");

        resumeButton.clicked += OnResumeButtonClicked;
        settingsButton.clicked += OnSettingsButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;
    }

    void OnDisable()
    {
        resumeButton.clicked -= OnResumeButtonClicked;
        settingsButton.clicked -= OnSettingsButtonClicked;
        quitButton.clicked -= OnQuitButtonClicked;
    }

    void OnResumeButtonClicked()
    {
        ResumeButtonAction.Invoke();
    }

    void OnSettingsButtonClicked()
    {
        SettingsButtonAction.Invoke();
    }

    void OnQuitButtonClicked()
    {
        // TODO: When we setup scene management, move to the start menu
        Debug.LogWarning("Unimplemented Quit Button Clicked");
    }
}
