using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class WorldSelectionHandler : MonoBehaviour
{
    public UnityAction SettingsButtonAction = delegate { };
    public UnityAction NextButtonAction = delegate { };

    private Button nextButton;
    private RadioButtonGroup worldChoices;

    private List<string> worldList = new List<string> { "World 1", "World 2", "World 3", "World 4", "World 5" };

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        nextButton = rootVisualElement.Q<Button>("NextButton");
        worldChoices = rootVisualElement.Q<RadioButtonGroup>("Choices");
        worldChoices.choices = worldList;
      
        nextButton.clicked += OnNextButtonClicked;
    }

    void OnDisable()
    {
        nextButton.clicked -= OnNextButtonClicked;
    }

    void OnNextButtonClicked()
    {
        setWorld(worldChoices.value);
        NextButtonAction.Invoke();
    }
    public string getWorldSelectionString()
    {
        return worldList[worldChoices.value];
    }

    public int getWorldSelection()
    {
        return worldChoices.value;
    }

    public void setWorld(int world)
    {
        worldChoices.value = world;
    }
}
