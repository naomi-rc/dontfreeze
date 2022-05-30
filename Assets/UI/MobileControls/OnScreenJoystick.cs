using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class OnScreenJoystick : VisualElement
{
    public event UnityAction<Vector2> MoveEvent = delegate { };
    public new class UxmlFactory : UxmlFactory<OnScreenJoystick, UxmlTraits> { }

    private VisualElement joystickBase;
    private VisualElement joystickBackground;
    private VisualElement joystickHandle;
    private bool isDragging = false;

    public OnScreenJoystick() : base()
    {
        joystickBase = new VisualElement();
        joystickBase.style.width = 150;
        joystickBase.style.height = 150;
        joystickBase.style.paddingBottom = 10;
        joystickBase.style.paddingLeft = 10;
        joystickBase.style.paddingRight = 10;
        joystickBase.style.paddingTop = 10;
        joystickBase.style.alignItems = Align.Center;
        joystickBase.style.justifyContent = Justify.Center;

        joystickBackground = new VisualElement();
        joystickBackground.style.width = new StyleLength(new Length(100, LengthUnit.Percent));
        joystickBackground.style.height = new StyleLength(new Length(100, LengthUnit.Percent));
        joystickBackground.style.backgroundColor = (Color)new Color32(115, 115, 115, 255);
        joystickBackground.style.borderBottomLeftRadius = new StyleLength(new Length(100, LengthUnit.Percent));
        joystickBackground.style.borderBottomRightRadius = new StyleLength(new Length(100, LengthUnit.Percent));
        joystickBackground.style.borderTopLeftRadius = new StyleLength(new Length(100, LengthUnit.Percent));
        joystickBackground.style.borderTopRightRadius = new StyleLength(new Length(100, LengthUnit.Percent));
        joystickBackground.style.position = Position.Absolute;


        joystickHandle = new VisualElement();
        joystickHandle.style.width = new StyleLength(new Length(75, LengthUnit.Percent));
        joystickHandle.style.height = new StyleLength(new Length(75, LengthUnit.Percent));
        joystickHandle.style.backgroundColor = (Color)new Color32(166, 166, 166, 255);
        joystickHandle.style.borderBottomLeftRadius = new StyleLength(new Length(100, LengthUnit.Percent));
        joystickHandle.style.borderBottomRightRadius = new StyleLength(new Length(100, LengthUnit.Percent));
        joystickHandle.style.borderTopLeftRadius = new StyleLength(new Length(100, LengthUnit.Percent));
        joystickHandle.style.borderTopRightRadius = new StyleLength(new Length(100, LengthUnit.Percent));

        joystickBase.Add(joystickBackground);
        joystickBase.Add(joystickHandle);
        Add(joystickBase);

        joystickHandle.RegisterCallback<PointerDownEvent>(OnPress);
        joystickBase.RegisterCallback<PointerMoveEvent>(OnMove);
        joystickBase.RegisterCallback<PointerLeaveEvent>(OnLeave);
        joystickHandle.RegisterCallback<PointerUpEvent>(OnRelease);
    }

    void OnPress(PointerDownEvent evt)
    {
        isDragging = true;
    }

    void OnMove(PointerMoveEvent evt)
    {
        if (!isDragging)
            return;

        var joystickHandleMove = new Vector3();
        joystickHandleMove.x = evt.localPosition.x - (joystickBase.layout.width / 2);
        joystickHandleMove.y = evt.localPosition.y - (joystickBase.layout.height / 2);
        joystickHandleMove.x = Mathf.Clamp(joystickHandleMove.x, -(joystickBase.layout.width / 2), (joystickBase.layout.width / 2));
        joystickHandleMove.y = Mathf.Clamp(joystickHandleMove.y, -(joystickBase.layout.height / 2), (joystickBase.layout.height / 2));
        joystickHandle.transform.position = joystickHandleMove;

        var normalized = new Vector3();
        normalized.x = joystickHandleMove.x / (joystickBase.layout.width / 2);
        normalized.y = -(joystickHandleMove.y / (joystickBase.layout.height / 2));
        normalized.z = 0;
        MoveEvent.Invoke(normalized);
    }

    void OnLeave(PointerLeaveEvent evt)
    {
        isDragging = false;
        joystickHandle.transform.position = Vector3.zero;
        MoveEvent.Invoke(Vector3.zero);
    }

    void OnRelease(PointerUpEvent evt)
    {
        isDragging = false;
        joystickHandle.transform.position = Vector3.zero;
        MoveEvent.Invoke(Vector3.zero);
    }
}
