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
    private Button restartButton;
    private Button settingsButton;
    private Button quitButton;

    [SerializeField]
    private Location MainMenu = default;

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        resumeButton = rootVisualElement.Q<Button>("ResumeButton");
        restartButton = rootVisualElement.Q<Button>("RestartButton");
        settingsButton = rootVisualElement.Q<Button>("SettingsButton");
        quitButton = rootVisualElement.Q<Button>("QuitButton");

        resumeButton.clicked += OnResumeButtonClicked;
        restartButton.clicked += OnRestartButtonClicked;
        settingsButton.clicked += OnSettingsButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;
    }

    void OnDisable()
    {
        resumeButton.clicked -= OnResumeButtonClicked;
        restartButton.clicked -= OnRestartButtonClicked;
        settingsButton.clicked -= OnSettingsButtonClicked;
        quitButton.clicked -= OnQuitButtonClicked;
    }

    void OnResumeButtonClicked()
    {
        ResumeButtonAction.Invoke();
    }

    void OnRestartButtonClicked()
    {
        // TODO: When we setup scene management, restart the game
        Debug.LogWarning("Unimplemented Restart Button Clicked");
    }

    void OnSettingsButtonClicked()
    {
        SettingsButtonAction.Invoke();
    }

    void OnQuitButtonClicked()
    {
        MainMenu.Load();
    }
}
