using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayUIHandler : MonoBehaviour
{
    [SerializeField]
    private PauseMenuHandler pauseMenuHandler;

    [SerializeField]
    private SettingsMenuHandler settingsMenuHandler;

    [SerializeField]
    private InputReader inputReader;

    void Awake()
    {
        inputReader.EnableGameplayInput();
        DisableMenus();

        inputReader.PauseEvent += OnPause;
        pauseMenuHandler.ResumeButtonAction += OnPauseResumeButtonClicked;
        pauseMenuHandler.SettingsButtonAction += OnPauseSettingsButtonClicked;
        settingsMenuHandler.OnSettingsBackButtonClicked += OnSettingsBackButtonClicked;
    }

    void OnPause()
    {
        inputReader.EnableUiInput();
        EnablePauseMenu();
    }

    void OnPauseResumeButtonClicked()
    {
        inputReader.EnableGameplayInput();
        DisableMenus();
    }

    void OnPauseSettingsButtonClicked()
    {
        inputReader.EnableUiInput();
        EnableSettingsMenu();
    }

    void OnSettingsBackButtonClicked()
    {
        inputReader.EnableGameplayInput();
        EnablePauseMenu();
    }

    void EnablePauseMenu()
    {
        // TODO: Disable HUD when we have it
        pauseMenuHandler.gameObject.SetActive(true);
        settingsMenuHandler.gameObject.SetActive(false);
    }

    void EnableSettingsMenu()
    {
        // TODO: Disable HUD when we have it
        pauseMenuHandler.gameObject.SetActive(false);
        settingsMenuHandler.gameObject.SetActive(true);
    }

    void DisableMenus()
    {
        // TODO: Enable HUD when we have it
        pauseMenuHandler.gameObject.SetActive(false);
        settingsMenuHandler.gameObject.SetActive(false);
    }
}
