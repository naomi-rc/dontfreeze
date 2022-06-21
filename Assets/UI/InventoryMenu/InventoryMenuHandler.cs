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
    private Button closeButton;
    private List<InventorySlot> slotElementsReferences = new List<InventorySlot>();
    private int selectedSlot = -1;
    private InventoryEntry draggedEntry;

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

        playerInventory.OnDatabaseChanged += OnInventoryChanged;
        OnInventoryChanged();

        closeButton.clicked += OnCloseButtonClicked;
    }

    void OnDisable()
    {
        inventory.UnregisterCallback<PointerDownEvent>(OnSlotClick, TrickleDown.TrickleDown);
        inventory.UnregisterCallback<PointerMoveEvent>(OnSlotDrag, TrickleDown.TrickleDown);
        inventory.UnregisterCallback<PointerUpEvent>(OnSlotRelease, TrickleDown.TrickleDown);
        playerInventory.OnDatabaseChanged -= OnInventoryChanged;
        closeButton.clicked -= OnCloseButtonClicked;
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
        if (evt.button != 0 || slotElement == null)
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
        if (slotElement == null)
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
                break;
            case InventoryConsumable _:
                type.text = "Consumable";
                break;
            default:
                type.text = "Item";
                break;
        }
        description.text = item.description;
    }
}
