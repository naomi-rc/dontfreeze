using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class PauseMenuHandler : MonoBehaviour
{
    public UnityAction ResumeButtonAction = delegate { };
    public UnityAction SettingsButtonAction = delegate { };
    public UnityAction<AudioClip> SubmitSoundAction = delegate { };

    private Button resumeButton;
    private Button restartButton;
    private Button settingsButton;
    private Button quitButton;

    [SerializeField]
    private Location MainMenu = default;

    [SerializeField]
    private AudioClip onHoverSound = default;

    [SerializeField]
    private AudioClip onClickSound = default;

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        resumeButton = rootVisualElement.Q<Button>("ResumeButton");
        resumeButton.RegisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        restartButton = rootVisualElement.Q<Button>("RestartButton");
        restartButton.RegisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        settingsButton = rootVisualElement.Q<Button>("SettingsButton");
        settingsButton.RegisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        quitButton = rootVisualElement.Q<Button>("QuitButton");
        quitButton.RegisterCallback<MouseEnterEvent>(OnButtonMouseEnter);

        resumeButton.style.opacity = 0;
        restartButton.style.opacity = 0;
        settingsButton.style.opacity = 0;
        quitButton.style.opacity = 0;

        resumeButton.clicked += OnResumeButtonClicked;
        restartButton.clicked += OnRestartButtonClicked;
        settingsButton.clicked += OnSettingsButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;

        StartCoroutine(FadeInAnimation());
    }

    void OnDisable()
    {
        resumeButton.clicked -= OnResumeButtonClicked;
        restartButton.clicked -= OnRestartButtonClicked;
        settingsButton.clicked -= OnSettingsButtonClicked;
        quitButton.clicked -= OnQuitButtonClicked;
        resumeButton.UnregisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        restartButton.UnregisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        settingsButton.UnregisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
        quitButton.UnregisterCallback<MouseEnterEvent>(OnButtonMouseEnter);
    }

    private IEnumerator FadeInAnimation()
    {
        yield return new WaitForSeconds(0.1f);
        resumeButton.style.opacity = 1;
        yield return new WaitForSeconds(0.1f);
        restartButton.style.opacity = 1;
        yield return new WaitForSeconds(0.1f);
        settingsButton.style.opacity = 1;
        yield return new WaitForSeconds(0.1f);
        quitButton.style.opacity = 1;
    }

    void OnResumeButtonClicked()
    {
        if (onClickSound != null)
        {
            SubmitSoundAction.Invoke(onClickSound);
        }
        ResumeButtonAction.Invoke();
    }

    void OnRestartButtonClicked()
    {
        if (onClickSound != null)
        {
            SubmitSoundAction.Invoke(onClickSound);
        }
        // TODO: When we setup scene management, restart the game
        Debug.LogWarning("Unimplemented Restart Button Clicked");
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
        MainMenu.Load();
    }

    void OnButtonMouseEnter(MouseEnterEvent _)
    {
        if (onHoverSound != null)
        {
            SubmitSoundAction.Invoke(onHoverSound);
        }
    }
}
