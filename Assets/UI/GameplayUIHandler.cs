using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

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
    private SceneEventChannel onLoadEvent;

    [SerializeField]
    private InputReader inputReader;

    [SerializeField]
    private Volume blurVolume;

    [SerializeField]
    private AudioSource audioSource;

    void Awake()
    {
        inputReader.EnableGameplayInput();
        DisableEverything();

        inputReader.PauseEvent += OnPause;
        inputReader.OpenInventoryEvent += OnOpenInventory;
        pauseMenuHandler.ResumeButtonAction += OnPauseResumeButtonClicked;
        pauseMenuHandler.SettingsButtonAction += OnPauseSettingsButtonClicked;
        pauseMenuHandler.SubmitSoundAction += OnSubmitSound;
        settingsMenuHandler.OnSettingsBackButtonClicked += OnSettingsBackButtonClicked;
        inventoryMenuHandler.OnInventoryCloseButtonClicked += OnInventoryCloseButtonClicked;
        onPlayerDeathEvent.OnEventRaised += OnPlayerDeath;
        onLoadEvent.OnEventRaised += OnLoadEvent;
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
        onLoadEvent.OnEventRaised -= OnLoadEvent;
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

    private IEnumerator Blur()
    {
        LeanTween.value(gameObject, 0.4f, 1f, 0.5f).setEase(LeanTweenType.easeInSine).setOnUpdate((float value) =>
        {
            blurVolume.weight = value;
        });

        yield return null;
    }

    private IEnumerator Unblur()
    {
        LeanTween.value(gameObject, 1f, 0f, 0.5f).setEase(LeanTweenType.easeOutSine).setOnUpdate((float value) =>
        {
            blurVolume.weight = value;
        });

        yield return null;
    }

    void OnPause()
    {
        inputReader.EnableUiInput();
        Time.timeScale = 0.0f;
        EnablePauseMenu();
        StartCoroutine(Blur());
    }

    void OnOpenInventory()
    {
        inputReader.EnableUiInput();
        Time.timeScale = 0.0f;
        EnableInventoryMenu();
        StartCoroutine(Blur());
    }

    void OnPauseResumeButtonClicked()
    {
        inputReader.EnableGameplayInput();
        DisableMenus();
        StartCoroutine(Unblur());
    }

    void OnPauseSettingsButtonClicked()
    {
        inputReader.EnableUiInput();
        EnableSettingsMenu();
    }

    void OnSettingsBackButtonClicked()
    {
        inputReader.EnableUiInput();
        EnablePauseMenu();
    }

    void OnInventoryCloseButtonClicked()
    {
        inputReader.EnableGameplayInput();
        DisableMenus();
        StartCoroutine(Unblur());
    }

    void OnPlayerDeath()
    {
        inputReader.EnableUiInput();
        EnableGameOverMenu();
    }

    void OnLoadEvent(SceneObject sceneToLoad)
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
            if (child.gameObject.name != "EventSystem" && child.gameObject.name != "BlurVolume" && child.gameObject.name != "AudioSource")
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    void DisableMenus()
    {
        DisableEverything();
        Time.timeScale = 1.0f;
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
        StartCoroutine(Blur());
    }

    void OnSubmitSound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
