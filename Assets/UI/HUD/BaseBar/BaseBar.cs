using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BaseBar : BindableElement, INotifyValueChanged<float>
{
    protected VisualElement background;
    protected VisualElement container;
    protected VisualElement progress;

    [SerializeField]
    private float m_value { get; set; } = 0f;
    public float value
    {
        get => m_value;
        set
        {
            if (this.enabledInHierarchy)
            {
                using (ChangeEvent<float> evt = ChangeEvent<float>.GetPooled(m_value, value))
                {
                    evt.target = this;
                    SetValueWithoutNotify(value);
                    SendEvent(evt);
                }
            }
            else
            {
                SetValueWithoutNotify(value);
            }
        }
    }

    public void SetValueWithoutNotify(float newValue)
    {
        m_value = Mathf.Clamp(newValue, 0f, 100f);
        UpdateWidth();
    }

    public new class UxmlTraits : VisualElement.UxmlTraits { }

    public new class UxmlFactory : UxmlFactory<BaseBar, UxmlTraits> { }

    public BaseBar() : base()
    {
        // Build the main container
        container = new VisualElement();
        container.name = "Container";
        container.style.flexDirection = FlexDirection.Row;
        container.style.height = Length.Percent(100f);
        container.style.width = Length.Percent(100f);
        container.AddToClassList("base-bar__container");

        // Build the background of the container
        background = new VisualElement();
        background.name = "Background";
        background.style.position = Position.Absolute;
        background.style.height = Length.Percent(100f);
        background.style.width = Length.Percent(100f);
        background.AddToClassList("base-bar__background");

        // Build the 'progress' bar
        progress = new VisualElement();
        progress.name = "Progress";
        progress.style.height = Length.Percent(100f);
        progress.AddToClassList("base-bar__progress");

        container.Add(background);
        container.Add(progress);

        Add(container);
    }

    private void UpdateWidth()
    {
        progress.style.width = Length.Percent(value);
    }
}

