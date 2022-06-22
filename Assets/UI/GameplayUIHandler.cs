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
    private HUDHandler HUDHandler;

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
        inventoryMenuHandler.OnInventoryCloseButtonClicked += OnInventoryCloseButtonClicked;
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

    void OnInventoryCloseButtonClicked()
    {
        inputReader.EnableGameplayInput();
        DisableMenus();
    }

    void EnablePauseMenu()
    {
        pauseMenuHandler.gameObject.SetActive(true);
        settingsMenuHandler.gameObject.SetActive(false);
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        mobileControlsDocument.SetActive(false);
#endif
        HUDHandler.gameObject.SetActive(false);
        inventoryMenuHandler.gameObject.SetActive(false);
    }

    void EnableInventoryMenu()
    {
        pauseMenuHandler.gameObject.SetActive(false);
        settingsMenuHandler.gameObject.SetActive(false);
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        mobileControlsDocument.SetActive(false);
#endif
        HUDHandler.gameObject.SetActive(false);
        inventoryMenuHandler.gameObject.SetActive(true);
    }

    void EnableSettingsMenu()
    {
        pauseMenuHandler.gameObject.SetActive(false);
        settingsMenuHandler.gameObject.SetActive(true);
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        mobileControlsDocument.SetActive(false);
#endif
        HUDHandler.gameObject.SetActive(false);
        inventoryMenuHandler.gameObject.SetActive(false);
    }

    void DisableMenus()
    {
        pauseMenuHandler.gameObject.SetActive(false);
        settingsMenuHandler.gameObject.SetActive(false);
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        mobileControlsDocument.SetActive(true);
#endif
        HUDHandler.gameObject.SetActive(true);
        inventoryMenuHandler.gameObject.SetActive(false);
    }
}
