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

        thirdButton.SetEnabled(false);
        thirdButton.AddToClassList("world-lock");

        fourthButton.SetEnabled(false);
        fourthButton.AddToClassList("world-lock");

        fifthButton.SetEnabled(false);
        fifthButton.AddToClassList("world-lock");
        
    }

    public void updateImage()
    {
        if (levelSettings.level1Complete)
        {
            secondButton.SetEnabled(true);
            secondButton.RemoveFromClassList("world-lock");
            if (levelSettings.level2Complete)
            {
                thirdButton.SetEnabled(true);
                thirdButton.RemoveFromClassList("world-lock");
                if (levelSettings.level3Complete)
                {
                    fourthButton.SetEnabled(true);
                    fourthButton.RemoveFromClassList("world-lock");
                    if (levelSettings.level4Complete)
                    {
                        fifthButton.SetEnabled(true);
                        fifthButton.RemoveFromClassList("world-lock");
                    }
                }
            }
        }
    }

    public int getWorldSelection()
    {
        if (firstButton.value)
        {
            levelNumber = 1;
        }
        if (secondButton.value)
        {
            levelNumber = 2;
        }
        if (thirdButton.value)
        {
            levelNumber = 3;
        }
        if (fourthButton.value)
        {
            levelNumber = 4;
        }
        if (fifthButton.value)
        {
            levelNumber = 5;
        }
        return levelNumber;
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
