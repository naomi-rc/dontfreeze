using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

class InventorySlot : VisualElement
{
    public new class UxmlFactory : UxmlFactory<InventorySlot, UxmlTraits> { }

    public InventoryItem item;

    private VisualElement itemIcon;
    private Label itemCount;

    public InventorySlot() : base()
    {
        AddToClassList("InventorySlot");
        style.alignItems = Align.Center;
        style.justifyContent = Justify.Center;

        itemIcon = new VisualElement();
        itemIcon.pickingMode = PickingMode.Ignore;
        itemIcon.style.width = new StyleLength(new Length(50, LengthUnit.Pixel));
        itemIcon.style.height = new StyleLength(new Length(50, LengthUnit.Pixel));

        itemCount = new Label();
        itemCount.pickingMode = PickingMode.Ignore;
        itemCount.style.position = Position.Absolute;
        itemCount.style.bottom = 0;
        itemCount.style.right = 0;
        itemCount.style.fontSize = new StyleLength(new Length(12, LengthUnit.Pixel));
        itemCount.style.unityFontStyleAndWeight = FontStyle.Bold;
        itemCount.style.color = Color.white;
        itemCount.text = "";

        Add(itemIcon);
        Add(itemCount);
    }

    public void SetItem(InventoryItem item, int count)
    {
        this.item = item;
        itemIcon.style.backgroundImage = new StyleBackground(item.icon);
        itemCount.text = count.ToString();
    }

    public void ClearItem()
    {
        itemIcon.style.backgroundImage = null;
        itemCount.text = "";
    }
}