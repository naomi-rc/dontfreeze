using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MainMenuHandler : MonoBehaviour
{
    public UnityAction SettingsButtonAction = delegate { };
    public UnityAction<AudioClip> SubmitSoundAction = delegate { };

    private Button playButton;
    private Button resumeButton;
    private Button settingsButton;
    private Button quitButton;

    [SerializeField]
    private Location startLocation = default;

    [SerializeField]
    private VoidEventChannel onLoadSaveEvent = default;

    [SerializeField]
    private AudioClip onHoverSound = default;

    [SerializeField]
    private AudioClip onClickSound = default;


    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        playButton = rootVisualElement.Q<Button>("PlayButton");
        resumeButton = rootVisualElement.Q<Button>("ResumeButton");
        settingsButton = rootVisualElement.Q<Button>("SettingsButton");
        quitButton = rootVisualElement.Q<Button>("QuitButton");

        playButton.clicked += OnPlayButtonClicked;
        playButton.RegisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        resumeButton.clicked += OnResumeButtonClicked;
        resumeButton.RegisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        settingsButton.clicked += OnSettingsButtonClicked;
        settingsButton.RegisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        quitButton.clicked += OnQuitButtonClicked;
        quitButton.RegisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
    }

    void OnDisable()
    {
        playButton.clicked -= OnPlayButtonClicked;
        playButton.UnregisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        resumeButton.clicked -= OnResumeButtonClicked;
        resumeButton.UnregisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        settingsButton.clicked -= OnSettingsButtonClicked;
        settingsButton.UnregisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        quitButton.clicked -= OnQuitButtonClicked;
        quitButton.UnregisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
    }

    void OnButtonMouseEnter(MouseEnterEvent _)
    {
        if (onHoverSound != null)
        {
            SubmitSoundAction.Invoke(onHoverSound);
        }
    }

    void OnPlayButtonClicked()
    {
        if (onClickSound != null)
        {
            SubmitSoundAction.Invoke(onClickSound);
        }
        startLocation.Load();
    }

    void OnResumeButtonClicked()
    {
        if (onClickSound != null)
        {
            SubmitSoundAction.Invoke(onClickSound);
        }
        onLoadSaveEvent.Raise();
    }

    void OnSettingsButtonClicked()
    {
        if (onClickSound != null)
        {
            SubmitSoundAction.Invoke(onClickSound);
        }
        SettingsButtonAction.Invoke();
    }

    void OnQuitButtonClicked()
    {
        if (onClickSound != null)
        {
            SubmitSoundAction.Invoke(onClickSound);
        }
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
