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

    private SliderInt enemyNumber;
    private Label enemyValue;
    private Label minEnemy;
    private Label maxEnemy;

    private RadioButtonGroup buttonGroup;

    private List<string> skyboxList = new List<string>{ "Daylight", "Nightlight", "DarkMoon", "MoonNight" };
    private List<string> difficultyList = new List<string> { "Easy", "Normal", "Hard" };

    private void Update()
    {
        updateValue();
    }

    private void updateValue()
    {
        enemyValue.text = getEnemyNumber().ToString();
        setMinMaxEnemyValue(buttonGroup.value);
    }

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        backButton = rootVisualElement.Q<Button>("BackButton");
        applyButton = rootVisualElement.Q<Button>("ApplyButton");

        skyboxDropDown = rootVisualElement.Q<DropdownField>("SkyboxList");
        skyboxDropDown.choices = skyboxList;
        skyboxDropDown.value = skyboxList[0];

        enemyNumber = rootVisualElement.Q<SliderInt>("EnemyNumber");
        enemyValue = rootVisualElement.Q<Label>("EnemyValue");
        enemyValue.text = enemyNumber.value.ToString();

        minEnemy = rootVisualElement.Q<Label>("Min");
        maxEnemy = rootVisualElement.Q<Label>("Max");

        buttonGroup = rootVisualElement.Q<RadioButtonGroup>("DifficultyGroup");
        buttonGroup.choices = difficultyList;
        buttonGroup.value = 1;

        setMinMaxEnemyValue(buttonGroup.value);

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
        enemyNumber.lowValue = value;
    }

    private void setMaxEnemyValue(int value)
    {
        enemyNumber.highValue = value;
    }

    private void setMinMaxValueText()
    {
        minEnemy.text = enemyNumber.lowValue.ToString();
        maxEnemy.text = enemyNumber.highValue.ToString();
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
        buttonGroup.value = difficulty;
        setMinMaxEnemyValue(difficulty);
    }

    private void setEnemyNumber(int number)
    {
        enemyNumber.value = number;
        enemyValue.text = number.ToString();
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
        return buttonGroup.value;
    }

    public string getStringDifficulty()
    {
        return difficultyList[buttonGroup.value];
    }
    
    public string getSkybox()
    {
        return skyboxDropDown.value;
    }

    public int getEnemyNumber()
    {
        return enemyNumber.value;
    }

    public void setValues(string skybox, int difficulty, int enemyNumber)
    {
        setDifficulty(difficulty);
        setSkybox(skybox);
        setEnemyNumber(enemyNumber);
    }
}
