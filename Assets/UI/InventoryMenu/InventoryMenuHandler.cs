using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Events;

public class InventoryMenuHandler : MonoBehaviour
{
    public UnityAction OnInventoryCloseButtonClicked = delegate { };

    [SerializeField]
    private InventoryDatabase playerInventory;

    private VisualElement inventory;
    private VisualElement sideBar;
    private VisualElement inspectCard;
    private VisualElement ghostIcon;
    private Button actionButton;
    private Button closeButton;
    private List<InventorySlot> slotElementsReferences = new List<InventorySlot>();
    private int selectedSlot = -1;
    private InventoryEntry draggedEntry;

    private InventorySlot clothes;
    private InventorySlot weapon;

    void Awake()
    {
        ghostIcon = new VisualElement();
        ghostIcon.style.position = Position.Absolute;
        ghostIcon.visible = false;
        ghostIcon.pickingMode = PickingMode.Ignore;
        ghostIcon.style.width = new StyleLength(new Length(50, LengthUnit.Pixel));
        ghostIcon.style.height = new StyleLength(new Length(50, LengthUnit.Pixel));

        for (int i = 0; i < playerInventory.Length; i++)
        {
            slotElementsReferences.Add(new InventorySlot());
        }
    }

    void OnEnable()
    {
        var rootElement = GetComponent<UIDocument>().rootVisualElement;
        inventory = rootElement.Q<VisualElement>("Inventory");
        inspectCard = rootElement.Q<VisualElement>("InspectCard");
        sideBar = rootElement.Q<VisualElement>("SideBar");
        closeButton = rootElement.Q<Button>("CloseButton");
        actionButton = rootElement.Q<Button>("ActionButton");
        clothes = rootElement.Q<InventorySlot>("Clothes");
        weapon = rootElement.Q<InventorySlot>("Weapon");
        rootElement.RegisterCallback<PointerDownEvent>(OnSlotClick, TrickleDown.TrickleDown);
        rootElement.RegisterCallback<PointerMoveEvent>(OnSlotDrag, TrickleDown.TrickleDown);
        rootElement.RegisterCallback<PointerUpEvent>(OnSlotRelease, TrickleDown.TrickleDown);

        if (selectedSlot != -1)
        {
            FocusItem(slotElementsReferences[selectedSlot].item);
        }
        else
        {
            sideBar.style.display = DisplayStyle.None;
        }

        rootElement.Add(ghostIcon);

        foreach (var slot in slotElementsReferences)
        {
            inventory.Add(slot);
        }

        clothes.SetItem(playerInventory.currentClothes);
        weapon.SetItem(playerInventory.currentWeapon);

        playerInventory.OnDatabaseChanged += OnInventoryChanged;
        OnInventoryChanged();

        closeButton.clicked += OnCloseButtonClicked;
        actionButton.clicked += OnActionButtonClicked;
    }

    void OnDisable()
    {
        inventory.UnregisterCallback<PointerDownEvent>(OnSlotClick, TrickleDown.TrickleDown);
        inventory.UnregisterCallback<PointerMoveEvent>(OnSlotDrag, TrickleDown.TrickleDown);
        inventory.UnregisterCallback<PointerUpEvent>(OnSlotRelease, TrickleDown.TrickleDown);
        playerInventory.OnDatabaseChanged -= OnInventoryChanged;
        closeButton.clicked -= OnCloseButtonClicked;
        actionButton.clicked -= OnActionButtonClicked;
    }

    void OnActionButtonClicked()
    {
        playerInventory.UseItem(selectedSlot);

        var entry = playerInventory.GetEntry(selectedSlot);
        if (entry == null)
        {
            FocusItem(null);
        }
        else if (playerInventory.canEquip(entry.item))
        {
            if (entry.item is InventoryClothes)
                clothes.ClearItem();
            if (entry.item is InventoryWeapon)
                weapon.ClearItem();


            actionButton.text = "Equip";
        }

        else if (playerInventory.canUnequip(entry.item))
        {
            if (entry.item is InventoryClothes)
                clothes.SetItem(entry.item);
            else if (entry.item is InventoryWeapon)
                weapon.SetItem(entry.item);


            actionButton.text = "Unequip";
        }

    }

    void OnCloseButtonClicked()
    {
        OnInventoryCloseButtonClicked.Invoke();
    }

    void OnInventoryChanged()
    {
        for (int i = 0; i < playerInventory.Length; i++)
        {
            var entry = playerInventory.GetEntry(i);
            if (entry != null)
            {
                slotElementsReferences[i].SetItem(entry.item, entry.count);
            }
            else
            {
                slotElementsReferences[i].ClearItem();
            }

            if (i == selectedSlot)
            {
                slotElementsReferences[i].AddToClassList("SelectedInventorySlot");
            }
            else
            {
                slotElementsReferences[i].RemoveFromClassList("SelectedInventorySlot");
            }
        }
    }

    private void OnSlotClick(PointerDownEvent evt)
    {
        var slotElement = evt.target as InventorySlot;
        if (evt.button != 0 || slotElement == null || slotElement == clothes || slotElement == weapon)
            return;

        var slotIndex = slotElementsReferences.IndexOf(slotElement);
        var entry = playerInventory.GetEntry(slotIndex);

        if (entry == null)
            return;

        selectedSlot = slotIndex;
        FocusItem(entry.item);
        draggedEntry = entry;

        ghostIcon.style.backgroundImage = entry.item.icon;
        ghostIcon.visible = true;
        ghostIcon.style.left = evt.position.x - (ghostIcon.layout.width / 2);
        ghostIcon.style.top = evt.position.y - (ghostIcon.layout.height / 2);
        OnInventoryChanged();
    }

    private void OnSlotDrag(PointerMoveEvent evt)
    {
        ghostIcon.style.left = evt.position.x - (ghostIcon.layout.width / 2);
        ghostIcon.style.top = evt.position.y - (ghostIcon.layout.height / 2);
    }

    private void OnSlotRelease(PointerUpEvent evt)
    {
        if (selectedSlot == -1 || draggedEntry == null)
            return;

        var slotElement = evt.target as InventorySlot;
        if (slotElement == null || slotElement == clothes || slotElement == weapon)
        {
            ghostIcon.visible = false;
            selectedSlot = -1;
            draggedEntry = null;
            OnInventoryChanged();
            return;
        }

        var slotIndex = slotElementsReferences.IndexOf(slotElement);
        playerInventory.MoveItems(selectedSlot, slotIndex);
        ghostIcon.visible = false;
        selectedSlot = slotIndex;
        draggedEntry = null;
        OnInventoryChanged();
    }

    void FocusItem(InventoryItem item)
    {
        if (item == null)
        {
            selectedSlot = -1;
            sideBar.style.display = DisplayStyle.None;
            return;
        }

        if (sideBar.style.display == DisplayStyle.None)
        {
            sideBar.style.display = DisplayStyle.Flex;
        }

        Label title = inspectCard.Q<Label>("Title");
        Label type = inspectCard.Q<Label>("Type");
        Label description = inspectCard.Q<Label>("Description");

        title.text = item.name;
        switch (item)
        {
            case InventoryWeapon _:
                type.text = "Weapon";
                actionButton.text = playerInventory.currentWeapon == item ? "Unequip" : "Equip";
                actionButton.visible = true;
                break;
            case InventoryClothes _:
                type.text = "Clothes";
                actionButton.text = playerInventory.currentClothes == item ? "Unequip" : "Equip";
                actionButton.visible = true;
                break;
            case InventoryConsumable _:
                type.text = "Consumable";
                actionButton.text = "Use";
                actionButton.visible = true;
                break;
            default:
                type.text = "Item";
                actionButton.visible = false;
                break;
        }
        description.text = item.description;
    }
}
