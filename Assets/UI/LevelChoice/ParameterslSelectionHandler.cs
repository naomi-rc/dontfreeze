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

    private RadioButton world1Button;
    private RadioButton world2Button;
    private RadioButton world3Button;
    private RadioButton world4Button;
    private RadioButton world5Button;

    private
    void OnEnable()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;

        backButton = rootVisualElement.Q<Button>("BackButton"); 
        applyButton = rootVisualElement.Q<Button>("ApplyButton");
        /*
        world1Button = rootVisualElement.Q<RadioButton>("World1Button");
        world2Button = rootVisualElement.Q<RadioButton>("World2Button");
        world3Button = rootVisualElement.Q<RadioButton>("World3Button");
        world4Button = rootVisualElement.Q<RadioButton>("World4Button");
        world5Button = rootVisualElement.Q<RadioButton>("World5Button");
        */
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
        //string world = getWorldSelection();
        Debug.Log("Bouton apply appuyé");
        ApplyButtonAction.Invoke();
    }  
}
