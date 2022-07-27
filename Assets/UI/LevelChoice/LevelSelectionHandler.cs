using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class LevelSelectionHandler : MonoBehaviour
{
    public UnityAction SettingsButtonAction = delegate { };
    public UnityAction NextButtonAction = delegate { };

    private Button nextButton;

    private RadioButton firstButton;
    private RadioButton secondButton;
    private RadioButton thirdButton;
    private RadioButton fourthButton;
    private RadioButton fifthButton;

    private int levelNumber = 1;

    public LevelSettings levelSettings;

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        nextButton = rootVisualElement.Q<Button>("NextButton");

        firstButton = rootVisualElement.Q<RadioButton>("FirstWorldButton");
        secondButton = rootVisualElement.Q<RadioButton>("SecondWorldButton");
        thirdButton = rootVisualElement.Q<RadioButton>("ThirdWorldButton");
        fourthButton = rootVisualElement.Q<RadioButton>("FourthWorldButton");
        fifthButton = rootVisualElement.Q<RadioButton>("FifthWorldButton");

        
        lockLevel();
        setLevel(levelNumber);

        nextButton.clicked += OnNextButtonClicked;
    }
   
    void OnDisable()
    {
        nextButton.clicked -= OnNextButtonClicked;
    }

    void OnNextButtonClicked()
    {
        levelNumber = getWorldSelection();
        NextButtonAction.Invoke();
    }

    public void lockLevel()
    {   
        secondButton.SetEnabled(false);
        secondButton.AddToClassList("world-lock");

        // Vérouiller le niveau 3
        thirdButton.SetEnabled(false);
        thirdButton.AddToClassList("world-lock");

        // Vérouiller le niveau 4
        fourthButton.SetEnabled(false);
        fourthButton.AddToClassList("world-lock");

        // Vérouiller le niveau 5
        fifthButton.SetEnabled(false);
        fifthButton.AddToClassList("world-lock");
        
    }

    public void updateImage()
    {
        // TODO Améliorer le code
        if (levelSettings.level1Complete)
        {
            // Dévérouiller le niveau 2
            secondButton.SetEnabled(true);
            secondButton.RemoveFromClassList("world-lock");
            if (levelSettings.level2Complete)
            {
                // Dévérouiller le niveau 3
                thirdButton.SetEnabled(true);
                thirdButton.RemoveFromClassList("world-lock");
                if (levelSettings.level3Complete)
                {
                    // Dévérouiller le niveau 4
                    fourthButton.SetEnabled(true);
                    fourthButton.RemoveFromClassList("world-lock");
                    if (levelSettings.level4Complete)
                    {
                        // Dévérouiller le niveau 5
                        fifthButton.SetEnabled(true);
                        fifthButton.RemoveFromClassList("world-lock");
                    }
                }
            }
        }
    }
    public int getWorldSelection()
    {
        int level = 1;

        if (firstButton.value)
        {
            level = 1;
        }
        if (secondButton.value)
        {
            level = 2;
        }
        if (thirdButton.value)
        {
            level = 3;
        }
        if (fourthButton.value)
        {
            level = 4;
        }
        if (fifthButton.value)
        {
            level = 5;
        }
        return level;
    }

    public void setLevel(int level)
    {
        levelNumber = level;
       
        if(level == 1)
        {
            firstButton.SetSelected(true);
        }
        if (level == 2)
        {
            secondButton.SetSelected(true);
        }
        if (level == 3)
        {
            thirdButton.SetSelected(true);
        }
        if (level == 4)
        {
            fourthButton.SetSelected(true);
        }
        if (level == 5)
        {
            fifthButton.SetSelected(true);
        }
    }

    private void Update()
    {
        updateImage();
    }
}
