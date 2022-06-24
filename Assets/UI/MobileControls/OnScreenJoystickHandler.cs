using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.InputSystem.Layouts;

public class OnScreenJoystickHandler : OnScreenControl
{
    [SerializeField]
    private string queryName = "";

    [InputControl(layout = "Vector2")]
    [SerializeField]
    private string m_ControlPath;

    private OnScreenJoystick onScreenJoystick;

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }

    override protected void OnEnable()
    {
        base.OnEnable();
        var rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        onScreenJoystick = rootVisualElement.Q<OnScreenJoystick>(queryName);

        if (onScreenJoystick == null)
        {
            Debug.LogError("OnScreenJoystick with name " + queryName + " not found");
            return;
        }

        onScreenJoystick.MoveEvent += OnMove;
    }

    protected override void OnDisable()
    {
        onScreenJoystick.MoveEvent -= OnMove;
        base.OnDisable();
    }

    private void OnMove(Vector2 move)
    {
        SendValueToControl(move);
    }
}
