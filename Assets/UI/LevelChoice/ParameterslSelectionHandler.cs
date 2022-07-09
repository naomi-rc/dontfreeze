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

    private Button backButton;
    private Button applyButton;
    private DropdownField skyboxDropDown;

    private RadioButton easyButton;
    private RadioButton normalButton;
    private RadioButton hardButton;

    private SliderInt enemyNumber;

    //private RadioButtonGroup buttonGroup;
   

    private List<string> skyboxList = new List<string>{ "Daylight", "Nightlight", "DarkMoon", "MoonNight" };

    private
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
        setMinEnemyValue(15); // Default minimum value

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
        Debug.Log("Difficulté : " + getDifficultyChoicie());
        Debug.Log("Skybox choisi : " + getSkybox());
        Debug.Log("Nombre d'enemy choisi : " + getEnemyNumber());
    }

    private void setMinEnemyValue(int value)
    {
        enemyNumber.lowValue = value;
    }

    private string getDifficultyChoicie()
    {
        string difficulty = "Normal";
        
        if (easyButton.value)
        {
            difficulty = "Easy";
            setMinEnemyValue(10);
        }
        if (normalButton.value)
        {
            difficulty = "Normal";
            setMinEnemyValue(15);
        }
        if (hardButton.value)
        {
            difficulty = "Hard";
            setMinEnemyValue(20);
        }
        
        return difficulty;
    }

    private string getSkybox()
    {
        string skybox = skyboxDropDown.value;
        return skybox;
    }

    private int getEnemyNumber()
    {
        return enemyNumber.value;
    }
}
