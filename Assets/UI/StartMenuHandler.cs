using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuHandler : MonoBehaviour
{
    [SerializeField]
    private MainMenuHandler mainMenuHandler;

    [SerializeField]
    private SettingsMenuHandler settingsMenuHandler;

    [SerializeField]
    private AudioSource audioSource;

    void Awake()
    {
        mainMenuHandler.SettingsButtonAction += OnSettingsButtonClicked;
        mainMenuHandler.SubmitSoundAction += OnSubmitSound;
        settingsMenuHandler.OnSettingsBackButtonClicked += OnSettingsBackButtonClicked;
        settingsMenuHandler.SubmitSoundAction += OnSubmitSound;
    }

    private void OnDisable()
    {
        mainMenuHandler.SettingsButtonAction -= OnSettingsButtonClicked;
        mainMenuHandler.SubmitSoundAction -= OnSubmitSound;
        settingsMenuHandler.OnSettingsBackButtonClicked -= OnSettingsBackButtonClicked;
        settingsMenuHandler.SubmitSoundAction -= OnSubmitSound;
    }

    void OnSettingsButtonClicked()
    {
        settingsMenuHandler.gameObject.SetActive(true);
    }

    void OnSettingsBackButtonClicked()
    {
        settingsMenuHandler.gameObject.SetActive(false);
    }

    void OnSubmitSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
