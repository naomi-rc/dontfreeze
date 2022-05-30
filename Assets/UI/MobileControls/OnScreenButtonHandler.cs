using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.InputSystem.Layouts;

public class OnScreenButtonHandler : OnScreenControl
{
    [SerializeField]
    private string queryName = "";

    [InputControl(layout = "Button")]
    [SerializeField]
    private string m_ControlPath;

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }

    void Awake()
    {
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        var onScreenButton = rootVisualElement.Q<Button>(queryName);

        if (onScreenButton == null)
        {
            Debug.LogError("OnScreenButton with name " + queryName + " not found");
            return;
        }

        onScreenButton.clicked += OnPointerDown;
        onScreenButton.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    private void OnPointerDown()
    {
        SendValueToControl(1.0f);
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        SendValueToControl(0.0f);
    }
}
