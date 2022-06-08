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

    private Button onScreenButton;

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }

    override protected void OnEnable()
    {
        base.OnEnable();
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        onScreenButton = rootVisualElement.Q<Button>(queryName);

        if (onScreenButton == null)
        {
            Debug.LogError("OnScreenButton with name " + queryName + " not found");
            return;
        }

        onScreenButton.clicked += OnPointerDown;
        onScreenButton.RegisterCallback<PointerUpEvent>(OnPointerUp);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        onScreenButton.UnregisterCallback<PointerUpEvent>(OnPointerUp);
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
