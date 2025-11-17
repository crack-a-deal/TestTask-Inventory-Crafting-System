using System.Collections.Generic;
using UnityEngine;

public class CraftingPagePresenter
{
    private readonly InventoryService _inventoryService;
    private readonly CraftingService craftingService;
    private readonly CraftingPageView _view;
    private readonly DragAndDropController _dragDropPresenter;
    private readonly TooltipPresenter tooltipPresenter;

    private Dictionary<SlotFrameView, ItemStack> _slots;
    private Dictionary<SlotFrameView, SlotFramePresenter> _slotsPresenter;


    public CraftingPagePresenter(InventoryService inventoryService, CraftingService craftingService, CraftingPageView view, DragAndDropController dragPresenter, TooltipPresenter tooltipPresenter)
    {
        _inventoryService = inventoryService;
        this.craftingService = craftingService;
        _view = view;
        _dragDropPresenter = dragPresenter;
        this.tooltipPresenter = tooltipPresenter;
        InitInventorySlots();

        _view.CraftButtonClicked += View_OnCraftButtonClicked;
    }

    private void InitInventorySlots()
    {
        _slots = new Dictionary<SlotFrameView, ItemStack>();
        _slotsPresenter = new Dictionary<SlotFrameView, SlotFramePresenter>();

        for (int i = 0; i < _view.Slots.Length; i++)
        {
            SlotFrameView slotFrame = _view.Slots[i];
            ItemStack itemSlot = craftingService.CraftingModel.Items[i];
            UISlot newSlot = new UISlot(i, craftingService.CraftingModel);
            SlotFramePresenter presenter = new SlotFramePresenter(newSlot, slotFrame, tooltipPresenter, _dragDropPresenter);

            _slots.Add(slotFrame, itemSlot);
            _slotsPresenter.Add(slotFrame, presenter);
        }
    }

    private void View_OnCraftButtonClicked()
    {

    }
}