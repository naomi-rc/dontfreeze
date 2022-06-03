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
    private GameObject mobileControlsDocument;

    [SerializeField]
    private InventoryMenuHandler inventoryMenuHandler;

    [SerializeField]
    private InputReader inputReader;

    void Awake()
    {
        inputReader.EnableGameplayInput();
        DisableMenus();

        inputReader.PauseEvent += OnPause;
        inputReader.OpenInventoryEvent += OnOpenInventory;
        pauseMenuHandler.ResumeButtonAction += OnPauseResumeButtonClicked;
        pauseMenuHandler.SettingsButtonAction += OnPauseSettingsButtonClicked;
        settingsMenuHandler.OnSettingsBackButtonClicked += OnSettingsBackButtonClicked;
    }

    void OnPause()
    {
        inputReader.EnableUiInput();
        EnablePauseMenu();
    }

    void OnOpenInventory()
    {
        inputReader.EnableUiInput();
        EnableInventoryMenu();
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
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        mobileControlsDocument.SetActive(false);
#endif
        inventoryMenuHandler.gameObject.SetActive(false);
    }

    void EnableInventoryMenu()
    {
        // TODO: Disable HUD when we have it
        pauseMenuHandler.gameObject.SetActive(false);
        settingsMenuHandler.gameObject.SetActive(false);
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        mobileControlsDocument.SetActive(false);
#endif
        inventoryMenuHandler.gameObject.SetActive(true);
    }

    void EnableSettingsMenu()
    {
        // TODO: Disable HUD when we have it
        pauseMenuHandler.gameObject.SetActive(false);
        settingsMenuHandler.gameObject.SetActive(true);
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        mobileControlsDocument.SetActive(false);
#endif
        inventoryMenuHandler.gameObject.SetActive(false);
    }

    void DisableMenus()
    {
        // TODO: Enable HUD when we have it
        pauseMenuHandler.gameObject.SetActive(false);
        settingsMenuHandler.gameObject.SetActive(false);
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        mobileControlsDocument.SetActive(true);
#endif
        inventoryMenuHandler.gameObject.SetActive(false);
    }
}
