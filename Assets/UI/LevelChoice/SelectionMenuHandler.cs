using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMenuHandler : MonoBehaviour
{
    [SerializeField]
    private ParameterslSelectionHandler parametersSelectionHandler;

    [SerializeField]
    private WorldSelectionHandler worldSelectionHandler;

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
        // TODO à compléter
        Debug.Log("Les paramètres du niveau sont : blablabla");
    }
    void OnNextButtonClicked()
    {
        worldSelectionHandler.gameObject.SetActive(false);
        parametersSelectionHandler.gameObject.SetActive(true);
    }

    void OnBackButtonClicked()
    {
        worldSelectionHandler.gameObject.SetActive(true);
        parametersSelectionHandler.gameObject.SetActive(false);
    }
}
