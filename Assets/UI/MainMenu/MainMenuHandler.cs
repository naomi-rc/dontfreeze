using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuHandler : MonoBehaviour
{
    private Button playButton;
    private Button settingsButton;
    private Button quitButton;

    void Awake()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        playButton = rootVisualElement.Q<Button>("PlayButton");
        settingsButton = rootVisualElement.Q<Button>("SettingsButton");
        quitButton = rootVisualElement.Q<Button>("QuitButton");

        playButton.clicked += OnPlayButtonClicked;
        settingsButton.clicked += OnSettingsButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;
    }

    void OnPlayButtonClicked()
    {
        Debug.Log("Play button clicked");
    }

    void OnSettingsButtonClicked()
    {
        Debug.Log("Settings button clicked");
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
