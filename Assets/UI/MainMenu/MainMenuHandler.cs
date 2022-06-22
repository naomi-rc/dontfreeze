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
    private Button settingsButton;
    private Button quitButton;

    [SerializeField]
    private LocationLoader locationLoader = default;

    [SerializeField]
    private SceneObject firstScene;

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        playButton = rootVisualElement.Q<Button>("PlayButton");
        settingsButton = rootVisualElement.Q<Button>("SettingsButton");
        quitButton = rootVisualElement.Q<Button>("QuitButton");

        playButton.clicked += OnPlayButtonClicked;
        settingsButton.clicked += OnSettingsButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;
    }

    void OnDisable()
    {
        playButton.clicked -= OnPlayButtonClicked;
        settingsButton.clicked -= OnSettingsButtonClicked;
        quitButton.clicked -= OnQuitButtonClicked;
    }

    void OnPlayButtonClicked()
    {
        locationLoader.Load(firstScene);
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
