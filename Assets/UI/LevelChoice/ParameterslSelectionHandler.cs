using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ParameterslSelectionHandler : MonoBehaviour
{
    public UnityAction BackButtonAction = delegate { };
    public UnityAction ApplyButtonAction = delegate { };

    private string difficulty;
    private  int enemyNumberChoice = 10;
    private  string skybox = "Daylight";

    private Button backButton;
    private Button applyButton;
    private DropdownField skyboxDropDown;

    private RadioButton easyButton;
    private RadioButton normalButton;
    private RadioButton hardButton;

    private SliderInt enemyNumber;

    private Label enemyValue;
    private Label minEnemy;
    private Label maxEnemy;

    private List<string> skyboxList = new List<string>{ "Daylight", "Nightlight", "DarkMoon", "MoonNight" };

    /* Ne fonctionne pas après avoir fait back puis next à nouveau
    private void Start()
    {
        enemyNumber.RegisterValueChangedCallback(x => updateValue());
        easyButton.RegisterValueChangedCallback(x => updateValue());
        normalButton.RegisterValueChangedCallback(x => updateValue());
        hardButton.RegisterValueChangedCallback(x => updateValue());
    }*/

    
    public void Update()
    {
        updateValue();
    }

    private void updateValue()
    {
        enemyValue.text = getEnemyNumber().ToString();
        getDifficultyChoice();
        
    }
    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        backButton = rootVisualElement.Q<Button>("BackButton");
        applyButton = rootVisualElement.Q<Button>("ApplyButton");

        skyboxDropDown = rootVisualElement.Q<DropdownField>("SkyboxList");
        skyboxDropDown.choices = skyboxList;
        skyboxDropDown.value = skyboxList[0]; // Default value
        
        easyButton = rootVisualElement.Q<RadioButton>("EasyButton");
        normalButton = rootVisualElement.Q<RadioButton>("NormalButton");
        hardButton = rootVisualElement.Q<RadioButton>("HardButton");

        enemyNumber = rootVisualElement.Q<SliderInt>("EnemyNumber");
        enemyValue = rootVisualElement.Q<Label>("EnemyValue");
        enemyValue.text = enemyNumberChoice.ToString();

        minEnemy = rootVisualElement.Q<Label>("Min");
        maxEnemy = rootVisualElement.Q<Label>("Max");

        getDifficultyChoice();

        //buttonGroup = rootVisualElement.Q<RadioButtonGroup>("DifficultyGroup");

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
        Debug.Log("Bouton back appuyé");
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

    private void setDifficulty(string difficulty)
    {
        if (difficulty == "Easy")
        {
            easyButton.SetSelected(true);
        }
        if (difficulty == "Normal")
        {
            normalButton.SetSelected(true);
        }
        if (difficulty == "Hard")
        {
            hardButton.SetSelected(true);
        }
        getDifficultyChoice();
    }

    private void setEnemyNumber(int number)
    {
        enemyNumber.value = number;
        enemyValue.text = number.ToString();
    }

    public string getDifficultyChoice()
    {
        string difficulty = "Normal";
        
        if (easyButton.value)
        {
            difficulty = "Easy";
            setMinEnemyValue(5);
            setMaxEnemyValue(15);
        }
        if (normalButton.value)
        {
            difficulty = "Normal";
            setMinEnemyValue(10);
            setMaxEnemyValue(20);
        }
        if (hardButton.value)
        {
            difficulty = "Hard";
            setMinEnemyValue(15);
            setMaxEnemyValue(30);
        }
        setMinMaxValueText();
        return difficulty;
    }
    
    public string getSkybox()
    {
        skybox = skyboxDropDown.value;
        return skybox;
    }

    public int getEnemyNumber()
    {
        enemyNumberChoice = enemyNumber.value;
        return enemyNumberChoice;
    }

    public void setValues(string skybox, string difficulty, int enemyNumber)
    {
        setDifficulty(difficulty);
        setSkybox(skybox);
        setEnemyNumber(enemyNumber);
    }

}
