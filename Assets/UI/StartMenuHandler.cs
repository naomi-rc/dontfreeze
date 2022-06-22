using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuHandler : MonoBehaviour
{
    [SerializeField]
    private MainMenuHandler mainMenuHandler;

    [SerializeField]
    private SettingsMenuHandler settingsMenuHandler;

    void Awake()
    {
        mainMenuHandler.SettingsButtonAction += OnSettingsButtonClicked;
        settingsMenuHandler.OnSettingsBackButtonClicked += OnSettingsBackButtonClicked;
    }

    private void OnDisable()
    {
        mainMenuHandler.SettingsButtonAction -= OnSettingsButtonClicked;
        settingsMenuHandler.OnSettingsBackButtonClicked -= OnSettingsBackButtonClicked;
    }

    void OnSettingsButtonClicked()
    {
        mainMenuHandler.gameObject.SetActive(false);
        settingsMenuHandler.gameObject.SetActive(true);
    }

    void OnSettingsBackButtonClicked()
    {
        mainMenuHandler.gameObject.SetActive(true);
        settingsMenuHandler.gameObject.SetActive(false);
    }
}
