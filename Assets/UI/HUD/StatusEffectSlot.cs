using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StatusEffectSlot : VisualElement
{
    public new class UxmlFactory : UxmlFactory<StatusEffectSlot, UxmlTraits> { }

    public VisualElement statusIcon;

    public StatusEffectSlot() : base()
    {
        AddToClassList("StatusEffectSlot");
        statusIcon = new VisualElement();

        // Todo: Change width / lenght values
        statusIcon.style.width = new StyleLength(new Length(50, LengthUnit.Pixel));
        statusIcon.style.height = new StyleLength(new Length(50, LengthUnit.Pixel));

        Add(statusIcon);
    }

    public void SetStatus(StatusEffect statusEffect)
    {
        statusIcon.style.backgroundImage = new StyleBackground(statusEffect.icon);
    }

    public void ClearStatus(StatusEffect statusEffect)
    {
        statusIcon.style.backgroundImage = null;
    }
}
