using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMenuHandler : MonoBehaviour
{
    [SerializeField]
    private ParametersSelectionHandler parametersSelectionHandler;

    [SerializeField]
    private LevelSelectionHandler levelSelectionHandler;

    [SerializeField]
    private LevelSettings levelSettings;

    [SerializeField]
    private Location startLocation = default;

    private string skybox;
    private int animalEnemyNumber;
    private int wispEnemyNumber;

    private int level;

    void Awake()
    {
        levelSelectionHandler.NextButtonAction += OnNextButtonClicked;
        parametersSelectionHandler.BackButtonAction += OnBackButtonClicked;
        parametersSelectionHandler.ApplyButtonAction += OnApplyButtonClicked;
    }

    private void OnDisable()
    {
        levelSelectionHandler.NextButtonAction -= OnNextButtonClicked;
        parametersSelectionHandler.BackButtonAction -= OnBackButtonClicked;
        parametersSelectionHandler.ApplyButtonAction -= OnApplyButtonClicked;
    }

    void OnApplyButtonClicked()
    {
        level = levelSelectionHandler.getWorldSelection();

        animalEnemyNumber = parametersSelectionHandler.getAnimalEnemyNumber();
        wispEnemyNumber = parametersSelectionHandler.getWispEnemyNumber();

        parametersSelectionHandler.setValues(level, animalEnemyNumber, wispEnemyNumber);
        levelSelectionHandler.setLevel(level);

        Debug.Log("The user chose the " + levelSelectionHandler.getWorldSelectionString() + " with the " 
            + skybox + " skybox. And the user chose " + animalEnemyNumber + "animal enemies and " + wispEnemyNumber + " wisp enemies!");
        Debug.Log("The user chose the level : " + level);

        levelSettings.wispNumber = wispEnemyNumber;
        levelSettings.animalNumber = animalEnemyNumber;
        
        levelSettings.levelNumber = level+1;

        startLocation.Load();
    }

    void OnNextButtonClicked()
    {
        levelSelectionHandler.gameObject.SetActive(false);
        parametersSelectionHandler.gameObject.SetActive(true);
        level = levelSelectionHandler.getWorldSelection();
        
        parametersSelectionHandler.setValues(level, animalEnemyNumber, wispEnemyNumber);
    }

    void OnBackButtonClicked()
    {
        levelSelectionHandler.gameObject.SetActive(true);
        parametersSelectionHandler.gameObject.SetActive(false);

        animalEnemyNumber = parametersSelectionHandler.getAnimalEnemyNumber();
        wispEnemyNumber = parametersSelectionHandler.getWispEnemyNumber();

        levelSelectionHandler.setLevel(level);
        parametersSelectionHandler.setSelectedLevel(level);
    }
}
