using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class ParametersSelectionHandler : MonoBehaviour
{
    public UnityAction BackButtonAction = delegate { };
    public UnityAction ApplyButtonAction = delegate { };
    public UnityAction<AudioClip> SubmitSoundAction = delegate { };


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

    [SerializeField]
    private AudioClip onHoverSound = default;

    [SerializeField]
    private AudioClip onClickSound = default;

    private int[] animalPair = new int[2];
    private int[] wispPair = new int[2];

    private int selectedLevel = 1;

    private void Update()
    {
        updateValue();
    }

    private void updateValue()
    {
        animalEnemyValue.text = getAnimalEnemyNumber().ToString();
        wispEnemyValue.text = getWispEnemyNumber().ToString();
    }

    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        backButton = rootVisualElement.Q<Button>("BackButton");
        backButton.RegisterCallback<MouseEnterEvent>(OnButtonMouseEnter);

        applyButton = rootVisualElement.Q<Button>("ApplyButton");
        applyButton.RegisterCallback<MouseEnterEvent>(OnButtonMouseEnter);


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

        createDifficultyPair();

        backButton.clicked += OnBackButtonClicked;
        applyButton.clicked += OnApplyButtonClicked;

    }

    void OnDisable()
    {
        backButton.clicked -= OnBackButtonClicked;
        backButton.UnregisterCallback<MouseEnterEvent>(OnButtonMouseEnter);

        applyButton.clicked -= OnApplyButtonClicked;
        backButton.UnregisterCallback<MouseEnterEvent>(OnButtonMouseEnter);

    }

    void OnBackButtonClicked()
    {
        if (onClickSound != null)
        {
            SubmitSoundAction.Invoke(onClickSound);
        }

        BackButtonAction.Invoke();
    }

    void OnApplyButtonClicked()
    {
        if (onClickSound != null)
        {
            SubmitSoundAction.Invoke(onClickSound);
        }

        ApplyButtonAction.Invoke();
    }
    void OnButtonMouseEnter(MouseEnterEvent _)
    {
        if (onHoverSound != null)
        {
            SubmitSoundAction.Invoke(onHoverSound);
        }
    }

    private void setMinMaxValueText()
    {
        minAnimalEnemy.text = animalEnemyNumber.lowValue.ToString();
        maxAnimalEnemy.text = animalEnemyNumber.highValue.ToString();

        minWispEnemy.text = wispEnemyNumber.lowValue.ToString();
        maxWispEnemy.text = wispEnemyNumber.highValue.ToString();
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

    public int getDifficultyChoice()
    {
        return selectedLevel;
    }
   
    public int getAnimalEnemyNumber()
    {
        return animalEnemyNumber.value;
    }

    public int getWispEnemyNumber()
    {
        return wispEnemyNumber.value;
    }

    public void setValues(int level, int animalEnemyNumber, int wispEnemyNumber)
    {
        setSelectedLevel(level);
        setAnimalEnemyNumber(animalEnemyNumber);
        setWispEnemyNumber(wispEnemyNumber);
    }

    public void setSelectedLevel(int level)
    {
        selectedLevel = level;
        createDifficultyPair();
    }

    public void setMinMaxEnemyValue()
    {
        animalEnemyNumber.lowValue = animalPair[0];
        animalEnemyNumber.highValue = animalPair[1];
        
        wispEnemyNumber.lowValue = wispPair[0];
        wispEnemyNumber.highValue = wispPair[1];

        setMinMaxValueText();
    }

    public void createDifficultyPair()
    {

        if(selectedLevel <= 3)
        {
            animalPair[0] = selectedLevel;
            animalPair[1] = selectedLevel + 2;

            wispPair[0] = 0;
            wispPair[1] = 1;
        }
        else
        {
            animalPair[0] = selectedLevel;
            animalPair[1] = selectedLevel + 2;

            wispPair[0] = 1;
            wispPair[1] = 3;
        }
        setMinMaxEnemyValue();
    }
}
