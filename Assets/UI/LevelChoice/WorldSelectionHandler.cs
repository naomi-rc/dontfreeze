using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class WorldSelectionHandler : MonoBehaviour
{
    public UnityAction SettingsButtonAction = delegate { };
    public UnityAction NextButtonAction = delegate { };

    private Button nextButton;

    private RadioButton world1Button;
    private RadioButton world2Button;
    private RadioButton world3Button;
    private RadioButton world4Button;
    private RadioButton world5Button;

    private 
    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        nextButton = rootVisualElement.Q<Button>("NextButton");
        world1Button = rootVisualElement.Q< RadioButton > ("World1Button");
        world2Button = rootVisualElement.Q<RadioButton>("World2Button");
        world3Button = rootVisualElement.Q<RadioButton>("World3Button");
        world4Button = rootVisualElement.Q<RadioButton>("World4Button");
        world5Button = rootVisualElement.Q<RadioButton>("World5Button");

        nextButton.clicked += OnNextButtonClicked;
    }

    void OnDisable()
    {
        nextButton.clicked -= OnNextButtonClicked;
    }

    void OnNextButtonClicked()
    {
        string world = getWorldSelection();
        Debug.Log("Monde choisi : " + world);
        NextButtonAction.Invoke();
    }

    // TODO à améliorer
    private string getWorldSelection()
    {
        string world = "World1";
        if (world1Button.value)
        {
            world = "World1";
        }
        if (world2Button.value)
        {
            world = "World2";
        }
        if (world3Button.value)
        {
            world = "World3";
        }
        if (world4Button.value)
        {
            world = "World4";
        }
        if (world5Button.value)
        {
            world = "World5";
        }
        return world;
    }
}
