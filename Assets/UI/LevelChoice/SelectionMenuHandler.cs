using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMenuHandler : MonoBehaviour
{
    [SerializeField]
    private ParameterslSelectionHandler parametersSelectionHandler;

    [SerializeField]
    private WorldSelectionHandler worldSelectionHandler;

    private string skybox;
    private int enemyNumber;
    private int difficulty;

    private string world;

    void Awake()
    {
        worldSelectionHandler.NextButtonAction += OnNextButtonClicked;
        parametersSelectionHandler.BackButtonAction += OnBackButtonClicked;
        parametersSelectionHandler.ApplyButtonAction += OnApplyButtonClicked;
    }

    private void OnDisable()
    {
        worldSelectionHandler.NextButtonAction -= OnNextButtonClicked;
        parametersSelectionHandler.BackButtonAction -= OnBackButtonClicked;
        parametersSelectionHandler.ApplyButtonAction -= OnApplyButtonClicked;

    }

    void OnApplyButtonClicked()
    {
        world = worldSelectionHandler.getWorldSelection();
        difficulty = parametersSelectionHandler.getDifficultyChoice();
        skybox = parametersSelectionHandler.getSkybox();
        enemyNumber = parametersSelectionHandler.getEnemyNumber();
        
        parametersSelectionHandler.setValues(skybox, difficulty, enemyNumber);
        worldSelectionHandler.setWorld(world);

        Debug.Log("The user chose the " + world + " with the " + skybox + " skybox. The level of difficulty is " + parametersSelectionHandler.getStringDifficulty() + " and the user chose " + enemyNumber + " enemies!");
    }
    void OnNextButtonClicked()
    {
        worldSelectionHandler.gameObject.SetActive(false);
        parametersSelectionHandler.gameObject.SetActive(true);
        world = worldSelectionHandler.getWorldSelection();

        parametersSelectionHandler.setValues(skybox, difficulty, enemyNumber);
    }

    void OnBackButtonClicked()
    {
        worldSelectionHandler.gameObject.SetActive(true);
        parametersSelectionHandler.gameObject.SetActive(false);

        difficulty = parametersSelectionHandler.getDifficultyChoice();
        skybox = parametersSelectionHandler.getSkybox();
        enemyNumber = parametersSelectionHandler.getEnemyNumber();

        worldSelectionHandler.setWorld(world);
    }
}
