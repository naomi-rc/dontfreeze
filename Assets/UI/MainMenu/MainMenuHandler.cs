using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MainMenuHandler : MonoBehaviour
{
    public UnityAction SettingsButtonAction = delegate { };

    private Button playButton;
    private Button resumeButton;
    private Button settingsButton;
    private Button quitButton;

    [SerializeField]
    private Location startLocation = default;

    [SerializeField]
    private VoidEventChannel onLoadSaveEvent = default;

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        playButton = rootVisualElement.Q<Button>("PlayButton");
        resumeButton = rootVisualElement.Q<Button>("ResumeButton");
        settingsButton = rootVisualElement.Q<Button>("SettingsButton");
        quitButton = rootVisualElement.Q<Button>("QuitButton");

        playButton.clicked += OnPlayButtonClicked;
        resumeButton.clicked += OnResumeButtonClicked;
        settingsButton.clicked += OnSettingsButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;
    }

    void OnDisable()
    {
        playButton.clicked -= OnPlayButtonClicked;
        resumeButton.clicked -= OnResumeButtonClicked;
        settingsButton.clicked -= OnSettingsButtonClicked;
        quitButton.clicked -= OnQuitButtonClicked;
    }

    void OnPlayButtonClicked()
    {
        startLocation.Load();
    }

    void OnResumeButtonClicked()
    {
        onLoadSaveEvent.Raise();
    }

    void OnSettingsButtonClicked()
    {
        SettingsButtonAction.Invoke();
    }

    void OnQuitButtonClicked()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
