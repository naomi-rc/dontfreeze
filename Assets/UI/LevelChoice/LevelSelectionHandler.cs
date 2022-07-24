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

    private RadioButtonGroup worldChoices;
    private int levelNumber = 0;

    public LevelSettings levelSettings;

    // TODO Peut-être utiliser la liste des niveaux de LevelManager à la place
    private List<string> levelList = new List<string> { "First world", "Second world", "Third world", "Fourth world", "Fifth world" };
    private List<RadioButton> radioButtons = new List<RadioButton>();
    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        nextButton = rootVisualElement.Q<Button>("NextButton");
        //worldChoices = rootVisualElement.Q<RadioButtonGroup>("WorldChoices");

        firstButton = rootVisualElement.Q<RadioButton>("FirstWorldButton");
        secondButton = rootVisualElement.Q<RadioButton>("SecondWorldButton");
        thirdButton = rootVisualElement.Q<RadioButton>("ThirdWorldButton");
        fourthButton = rootVisualElement.Q<RadioButton>("FourthWorldButton");
        fifthButton = rootVisualElement.Q<RadioButton>("FifthWorldButton");

        radioButtons.Add(firstButton);
        radioButtons.Add(secondButton);
        radioButtons.Add(thirdButton);
        radioButtons.Add(fourthButton);
        radioButtons.Add(fifthButton);
        lockLevel();
        setLevel(levelNumber);

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
        //setLevel(worldChoices.value);
        
        levelNumber = getWorldSelection();
        setLevel(levelNumber);

        NextButtonAction.Invoke();
    }
    
    public string getWorldSelectionString()
    {
        //return worldList[worldChoices.value];
        return levelList[getWorldSelection()];
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
        // TODO changer pour un event system par exemple
        if (levelSettings.world1Complete)
        {
            // Dévérouiller le niveau 2
            secondButton.SetEnabled(true);
            secondButton.RemoveFromClassList("world-lock");
            if (levelSettings.world2Complete)
            {
                // Dévérouiller le niveau 3
                thirdButton.SetEnabled(true);
                thirdButton.RemoveFromClassList("world-lock");
                if (levelSettings.world3Complete)
                {
                    // Dévérouiller le niveau 4
                    fourthButton.SetEnabled(true);
                    fourthButton.RemoveFromClassList("world-lock");
                    if (levelSettings.world4Complete)
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
        int i;
        for (i = 0; i < radioButtons.Count; i++)
        {
            if (radioButtons[i].value)
            {
                return i;
            }
        }
        return i;
    }

    public void setLevel(int level)
    {
        for (int i = 0; i < radioButtons.Count; ++i)
        {
            if (i == level)
            {
                // TODO corriger l'image de sélection ne n'affiche pas
                radioButtons[i].SetSelected(true);
                //radioButtons[i].value = true;
                //radioButtons[i].SetValueWithoutNotify(true);
                levelNumber = i;
                return;
            }  
        }
    }

    private void Update()
    {
        updateImage();
    }
}
