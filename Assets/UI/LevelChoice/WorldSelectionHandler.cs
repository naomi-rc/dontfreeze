using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class WorldSelectionHandler : MonoBehaviour
{
    public UnityAction SettingsButtonAction = delegate { };
    public UnityAction NextButtonAction = delegate { };

    private Button nextButton;

    private string world;

    private RadioButton firstButton;
    private RadioButton secondButton;
    private RadioButton thirdButton;
    private RadioButton fourthButton;
    private RadioButton fifthButton;

    private RadioButtonGroup worldChoices;

    //private List<string> worldList = new List<string> { "World 1", "World 2", "World 3", "World 4", "World 5" };

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        nextButton = rootVisualElement.Q<Button>("NextButton");
        worldChoices = rootVisualElement.Q<RadioButtonGroup>("WorldChoices");

        firstButton = rootVisualElement.Q<RadioButton>("FirstWorldButton");
        secondButton = rootVisualElement.Q<RadioButton>("SecondWorldButton");
        thirdButton = rootVisualElement.Q<RadioButton>("ThirdWorldButton");
        fourthButton = rootVisualElement.Q<RadioButton>("FourthWorldButton");
        fifthButton = rootVisualElement.Q<RadioButton>("FifthWorldButton");

        //worldChoices.choices = worldList;
        //worldChoices.value = 0;
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
        //return worldList[worldChoices.value];
        return world;
    }


    public int getWorldSelection()
    {
        int i = 0;

        if (firstButton.value)
        {
            Debug.Log("Premier monde sélectionner");
            this.world = "First world";
        }
        if (secondButton.value)
        {
            Debug.Log("Deuxième monde sélectionner");
            this.world = "Second world";
        }

        //return worldChoices.value;
        return i;
    }

    public void setWorld(int world)
    {
        worldChoices.value = world;
    }
}
