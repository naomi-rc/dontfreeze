using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ParametersSelectionHandler : MonoBehaviour
{
    public UnityAction BackButtonAction = delegate { };
    public UnityAction ApplyButtonAction = delegate { };

    private DropdownField skyboxDropDown;

    private Button backButton;
    private Button applyButton;

    private SliderInt animalEnemyNumber;
    private SliderInt wispEnemyNumber;

    private Label animalEnemyValue;
    private Label wispEnemyValue;

    private Label minAnimalEnemy;
    private Label maxAnimalEnemy;

    private Label minWispEnemy;
    private Label maxWispEnemy;

    private List<DifficultyPair> animalPair;
    private List<DifficultyPair> wispPair;

    //private RadioButtonGroup buttonGroup;

    private List<string> skyboxList = new List<string>{ "Daylight", "Nightlight", "DarkMoon", "MoonNight" };
    private List<string> difficultyList = new List<string> { "Easy", "Normal", "Hard" };

    private void Update()
    {
        updateValue();
    }

    private void updateValue()
    {
        animalEnemyValue.text = getAnimalEnemyNumber().ToString();
        wispEnemyValue.text = getWispEnemyNumber().ToString();
        //setMinMaxEnemyValue(buttonGroup.value);
    }

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        backButton = rootVisualElement.Q<Button>("BackButton");
        applyButton = rootVisualElement.Q<Button>("ApplyButton");

        skyboxDropDown = rootVisualElement.Q<DropdownField>("SkyboxList");
        skyboxDropDown.choices = skyboxList;
        skyboxDropDown.value = skyboxList[0];

        animalEnemyNumber = rootVisualElement.Q<SliderInt>("AnimalEnemyNumber");
        animalEnemyValue = rootVisualElement.Q<Label>("AnimalEnemyValue");
        animalEnemyValue.text = animalEnemyNumber.value.ToString();

        wispEnemyNumber = rootVisualElement.Q<SliderInt>("WispEnemyNumber");
        wispEnemyValue = rootVisualElement.Q<Label>("WispEnemyValue");
        wispEnemyValue.text = wispEnemyNumber.value.ToString();

        minAnimalEnemy = rootVisualElement.Q<Label>("MinAnimal");
        maxAnimalEnemy = rootVisualElement.Q<Label>("MaxAnimal");

        minWispEnemy = rootVisualElement.Q<Label>("MinWisp");
        maxWispEnemy = rootVisualElement.Q<Label>("MaxWisp");

        // TODO Remplacer par autre chose
        //buttonGroup = rootVisualElement.Q<RadioButtonGroup>("DifficultyGroup");
        //buttonGroup.choices = difficultyList;
        //buttonGroup.value = 1;

        //setMinMaxEnemyValue(buttonGroup.value);

        backButton.clicked += OnBackButtonClicked;
        applyButton.clicked += OnApplyButtonClicked;
    }

    void OnDisable()
    {
        backButton.clicked -= OnBackButtonClicked;
        applyButton.clicked -= OnApplyButtonClicked;
    }

    void OnBackButtonClicked()
    {
        BackButtonAction.Invoke();
    }

    void OnApplyButtonClicked()
    {
        ApplyButtonAction.Invoke();
    }

    private void setMinEnemyValue(int value)
    {
        animalEnemyNumber.lowValue = value;
    }

    private void setMaxEnemyValue(int value)
    {
        animalEnemyNumber.highValue = value;
    }

    private void setMinMaxValueText()
    {
        minAnimalEnemy.text = animalEnemyNumber.lowValue.ToString();
        maxAnimalEnemy.text = animalEnemyNumber.highValue.ToString();

        minWispEnemy.text = animalEnemyNumber.lowValue.ToString();
        maxWispEnemy.text = animalEnemyNumber.highValue.ToString();
    }

    private void setSkybox(string skybox)
    {
        for(int i = 0; i < skyboxList.Count; i++)
        {
            if (skyboxList[i].ToString() == skybox)
            {
                skyboxDropDown.value = skyboxList[i];
                return;
            }
        }
    }

    private void setDifficulty(int difficulty)
    {
        //buttonGroup.value = difficulty;
        setMinMaxEnemyValue(difficulty);
    }

    private void setAnimalEnemyNumber(int number)
    {
        animalEnemyNumber.value = number;
        animalEnemyValue.text = number.ToString();
    }

    private void setWispEnemyNumber(int number)
    {
        wispEnemyNumber.value = number;
        wispEnemyValue.text = number.ToString();
    }

    public void setMinMaxEnemyValue(int difficulty)
    {
        if (difficulty == 0)
        {
            setMinEnemyValue(5);
            setMaxEnemyValue(15);
        }
        if (difficulty == 1)
        {
            setMinEnemyValue(10);
            setMaxEnemyValue(20);
        }
        if (difficulty == 2)
        {
            setMinEnemyValue(15);
            setMaxEnemyValue(30);
        }
        setMinMaxValueText();
    }

    public int getDifficultyChoice()
    {
        return 1;
        //return buttonGroup.value;
    }

    public string getStringDifficulty()
    {
        //return difficultyList[buttonGroup.value];
        return difficultyList[1];
    }
    
    public string getSkybox()
    {
        return skyboxDropDown.value;
    }

    public int getAnimalEnemyNumber()
    {
        return animalEnemyNumber.value;
    }

    public int getWispEnemyNumber()
    {
        return wispEnemyNumber.value;
    }

    public void setValues(string skybox, int difficulty, int animalEnemyNumber, int wispEnemyNumber)
    {
        //setDifficulty(difficulty);
        setSkybox(skybox);
        setAnimalEnemyNumber(animalEnemyNumber);
        setWispEnemyNumber(wispEnemyNumber);
    }
}
