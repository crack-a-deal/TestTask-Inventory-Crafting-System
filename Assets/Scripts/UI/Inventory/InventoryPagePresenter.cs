using System;
using System.Collections.Generic;
using System.Linq;

public class InventoryPagePresenter
{
    private readonly InventoryService _inventoryService;
    private readonly InventoryPageView _view;
    private readonly DragAndDropController _dragDropPresenter;
    private readonly ItemDatabase _items;
    private readonly TooltipPresenter _tooltipPresenter;


    private Dictionary<SlotFrameView, ItemStack> _slots;
    private Dictionary<SlotFrameView, SlotFramePresenter> _slotsPresenter;

    private ItemStack _emptyItem = new ItemStack(null, 0);

    public InventoryPagePresenter(InventoryService inventoryService, InventoryPageView view, DragAndDropController dragPresenter, ItemDatabase items, TooltipPresenter tooltipPresenter)
    {
        _inventoryService = inventoryService;
        _view = view;
        _dragDropPresenter = dragPresenter;
        _items = items;
        _tooltipPresenter = tooltipPresenter;

        InitInventorySlots();

        _view.FillButtonClicked += View_OnFillButtonClicked;
        _view.ClearButtonClicked += View_OnClearButtonClicked;

        _view.AllTypeButtonClicked += View_OnAllTypeButtonClicked;
        _view.ResourceButtonClicked += View_OnResourceButtonClicked;
        _view.ToolButtonClicked += View_OnToolButtonClicked;
    }

    private void InitInventorySlots()
    {
        _slots = new Dictionary<SlotFrameView, ItemStack>();
        _slotsPresenter = new Dictionary<SlotFrameView, SlotFramePresenter>();

        for (int i = 0; i < _view.Slots.Length; i++)
        {
            SlotFrameView slotFrame = _view.Slots[i];
            ItemStack itemSlot = _inventoryService.Inventory.Items[i];
            UISlot newSlot = new UISlot(i, _inventoryService.Inventory);
            SlotFramePresenter presenter = new SlotFramePresenter(newSlot, slotFrame, _tooltipPresenter, _dragDropPresenter);

            _slots.Add(slotFrame, itemSlot);
            _slotsPresenter.Add(slotFrame, presenter);
        }
    }

    private void View_OnFillButtonClicked()
    {
        _inventoryService.Fill(_items.GetAllItems());
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