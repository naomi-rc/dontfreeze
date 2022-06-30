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
    private GameOverMenuHandler gameOverMenuHandler;

    [SerializeField]
    private SceneTransitionHandler sceneTransitionHandler;

    [SerializeField]
    private VoidEventChannel onPlayerDeathEvent;

    [SerializeField]
    private VoidEventChannel onLoadingRequest;

    [SerializeField]
    private InputReader inputReader;

    void Awake()
    {
        inputReader.EnableGameplayInput();
        DisableEverything();

        inputReader.PauseEvent += OnPause;
        inputReader.OpenInventoryEvent += OnOpenInventory;
        pauseMenuHandler.ResumeButtonAction += OnPauseResumeButtonClicked;
        pauseMenuHandler.SettingsButtonAction += OnPauseSettingsButtonClicked;
        settingsMenuHandler.OnSettingsBackButtonClicked += OnSettingsBackButtonClicked;
        inventoryMenuHandler.OnInventoryCloseButtonClicked += OnInventoryCloseButtonClicked;
        onPlayerDeathEvent.OnEventRaised += OnPlayerDeath;
        onLoadingRequest.OnEventRaised += OnLoadingRequest;
    }

    private void OnDisable()
    {
        inputReader.PauseEvent -= OnPause;
        inputReader.OpenInventoryEvent -= OnOpenInventory;
        pauseMenuHandler.ResumeButtonAction -= OnPauseResumeButtonClicked;
        pauseMenuHandler.SettingsButtonAction -= OnPauseSettingsButtonClicked;
        settingsMenuHandler.OnSettingsBackButtonClicked -= OnSettingsBackButtonClicked;
        inventoryMenuHandler.OnInventoryCloseButtonClicked -= OnInventoryCloseButtonClicked;
        onPlayerDeathEvent.OnEventRaised -= OnPlayerDeath;
        onLoadingRequest.OnEventRaised -= OnLoadingRequest;
    }

    private IEnumerator Start()
    {
        sceneTransitionHandler.gameObject.SetActive(true);
        yield return sceneTransitionHandler.FadeInAnimation();

        HUDHandler.gameObject.SetActive(true);
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        mobileControlsDocument.SetActive(true);
#endif
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

    void OnPlayerDeath()
    {
        inputReader.EnableUiInput();
        EnableGameOverMenu();
    }

    void OnLoadingRequest()
    {
        DisableEverything();
        inputReader.DisableInput();

        sceneTransitionHandler.gameObject.SetActive(true);
        sceneTransitionHandler.FadeOut();
    }

    void DisableEverything()
    {
        var children = gameObject.GetComponentInChildren<Transform>();

        foreach (Transform child in children)
        {
            //  || child.gameObject.name == sceneTransitionHandler.gameObject.name)s
            if (child.gameObject.name != "EventSystem")
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    void DisableMenus()
    {
        DisableEverything();
        HUDHandler.gameObject.SetActive(true);
#if UNITY_ANDROID || UNITY_IOS || UNITY_EDITOR
        mobileControlsDocument.SetActive(true);
#endif
    }

    void EnablePauseMenu()
    {
        DisableEverything();
        pauseMenuHandler.gameObject.SetActive(true);
    }

    void EnableInventoryMenu()
    {
        DisableEverything();
        inventoryMenuHandler.gameObject.SetActive(true);
    }

    void EnableSettingsMenu()
    {
        DisableEverything();
        settingsMenuHandler.gameObject.SetActive(true);
    }

    void EnableGameOverMenu()
    {
        DisableEverything();
        gameOverMenuHandler.gameObject.SetActive(true);
    }
}
