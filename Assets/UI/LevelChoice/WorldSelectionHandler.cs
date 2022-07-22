using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class WorldSelectionHandler : MonoBehaviour
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
    
    //public Sprite worldNotSelected;
    //public Sprite worldSelected;

    public LevelSettings levelSettings;


    private List<string> worldList = new List<string> { "First world", "Second world", "Third world", "Fourth world", "Fifth world" };
    private List<RadioButton> radioButtons = new List<RadioButton>();
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

        radioButtons.Add(firstButton);
        radioButtons.Add(secondButton);
        radioButtons.Add(thirdButton);
        radioButtons.Add(fourthButton);
        radioButtons.Add(fifthButton);
        lockLevel();

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
        return worldList[getWorldSelection()];
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
        /*
        if (firstButton.value)
        {
            i = 0;
        }
        if (secondButton.value)
        {
            i = 1;
        }
        if (thirdButton.value)
        {
            i = 2;
        }
        if (fourthButton.value)
        {
            i = 3;
        }
        if (fifthButton.value)
        {
            i = 4;
        }*/

        //return worldChoices.value;
        return i;
    }

    public void setWorld(int world)
    {
        worldChoices.value = world;
    }

    private void Update()
    {
        updateImage();
    }
}
