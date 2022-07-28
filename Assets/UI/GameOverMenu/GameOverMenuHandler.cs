using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverMenuHandler : MonoBehaviour
{
    private VisualElement screen;
    private Label title;
    private Button restartButton;
    private Button quitButton;

    void OnEnable()
    {
        var rootElement = GetComponent<UIDocument>().rootVisualElement;
        screen = rootElement.Q<VisualElement>("Screen");
        title = rootElement.Q<Label>("Title");
        restartButton = rootElement.Q<Button>("RestartButton");
        quitButton = rootElement.Q<Button>("QuitButton");

        title.style.opacity = 0;
        restartButton.style.opacity = 0;
        quitButton.style.opacity = 0;

        restartButton.clicked += OnRestartButtonClicked;
        quitButton.clicked += OnQuitButtonClicked;

        StartCoroutine(GameOverScreenAnimation());
    }

    void OnDisable()
    {
        restartButton.clicked -= OnRestartButtonClicked;
        quitButton.clicked -= OnQuitButtonClicked;
    }

    void OnRestartButtonClicked()
    {
        // TODO: When we setup scene management, restart the game
        Debug.LogWarning("Unimplemented Restart Button Clicked");
    }

    void OnQuitButtonClicked()
    {
        // TODO: When we setup scene management, quit to main menu
        Debug.LogWarning("Unimplemented Quit Button Clicked");
    }

    private IEnumerator GameOverScreenAnimation()
    {
        yield return ChangeBackgroundColor();
        yield return ChangeTitleOpacity();
        yield return ChangeButtonsOpacity();
    }

    IEnumerator ChangeBackgroundColor()
    {
        yield return new WaitForSeconds(0.1f);
        screen.style.backgroundColor = new StyleColor(new Color(0.0f, 0.0f, 0.0f, 0.9f));
    }

    IEnumerator ChangeTitleOpacity()
    {
        yield return new WaitForSeconds(2.0f);
        title.style.opacity = 1;
    }

    IEnumerator ChangeButtonsOpacity()
    {
        yield return new WaitForSeconds(1.0f);
        restartButton.style.opacity = 1;
        quitButton.style.opacity = 1;
    }
}
