using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class InventoryPagePresenter
{
    private readonly InventoryService _inventoryService;
    private readonly InventoryPageView _view;
    private readonly DragAndDropController _dragDropController;
    private readonly SlotFramePresenterFactory _factory;

    private Dictionary<SlotFrameView, SlotFramePresenter> _slotsPresenter;

    private readonly ItemStack _emptyItem = new ItemStack(null, 0);

    public InventoryPagePresenter(InventoryService inventoryService, InventoryPageView view,DragAndDropController dragDropController,  SlotFramePresenterFactory factory)
    {
        _inventoryService = inventoryService;
        _view = view;
        _dragDropController = dragDropController;
        _factory = factory;

        InitInventorySlots();

        _view.FillButtonClicked += View_OnFillButtonClicked;
        _view.ClearButtonClicked += View_OnClearButtonClicked;

        _view.AllTypeButtonClicked += View_OnAllTypeButtonClicked;
        _view.ResourceButtonClicked += View_OnResourceButtonClicked;
        _view.ToolButtonClicked += View_OnToolButtonClicked;

        _dragDropController.ItemDropped += DragDropController_OnItemDropped;
    }

    private void DragDropController_OnItemDropped(UISlot slot, Vector2 position)
    {
        bool isAllowArea = RectTransformUtility.RectangleContainsScreenPoint(_view.RectTransform, position);
        if (!isAllowArea)
        {
            _inventoryService.RemoveItem(slot.Index);
        }
    }

    private void InitInventorySlots()
    {
        _slotsPresenter = new Dictionary<SlotFrameView, SlotFramePresenter>();

        for (int i = 0; i < _view.Slots.Length; i++)
        {
            SlotFrameView slotFrame = _view.Slots[i];
            ItemStack itemSlot = _inventoryService.Inventory.Items[i];
            UISlot newSlot = new UISlot(i, _inventoryService.Inventory);
            SlotFramePresenter presenter = _factory.Create(newSlot, _view.Slots[i]);

            _slotsPresenter.Add(slotFrame, presenter);
        }
    }

    private void View_OnFillButtonClicked()
    {
        _inventoryService.FillRandom();
    }

    private void View_OnClearButtonClicked()
    {
        _inventoryService.Clear();
    }

    #region Tabs

    private void View_OnToolButtonClicked()
    {
        UpdateSlots(i => i.Item != null && i.Item.Type == ItemType.Tool);
    }

    private void View_OnResourceButtonClicked()
    {
        UpdateSlots(i => i.Item != null && i.Item.Type == ItemType.Resource);
    }

    private void View_OnAllTypeButtonClicked()
    {
        UpdateSlots(i => true);
    }

    private void UpdateSlots(Func<ItemStack, bool> filter)
    {
        ItemStack[] visibleItems = _inventoryService.Inventory.Items.Where(filter).ToArray();

        for (int i = 0; i < _view.Slots.Length; i++)
        {
            if (i < visibleItems.Length && visibleItems[i].Item != null)
            {
                _slotsPresenter[_view.Slots[i]].SetItem(visibleItems[i]);
            }
            else
            {
                _slotsPresenter[_view.Slots[i]].SetItem(_emptyItem);
            }
        }
    }
    #endregion
}