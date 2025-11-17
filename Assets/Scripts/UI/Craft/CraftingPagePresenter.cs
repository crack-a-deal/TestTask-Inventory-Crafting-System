using System.Collections.Generic;

public class CraftingPagePresenter
{
    private readonly CraftingService _craftingService;
    private readonly CraftingPageView _view;
    private readonly DragAndDropController _dragDropPresenter;
    private readonly TooltipPresenter _tooltipPresenter;

    private Dictionary<SlotFrameView, ItemStack> _slots;
    private Dictionary<SlotFrameView, SlotFramePresenter> _slotsPresenter;

    private SlotFramePresenter _craftingSlotPresenter;

    public CraftingPagePresenter(CraftingService craftingService, CraftingPageView view, DragAndDropController dragPresenter, TooltipPresenter tooltipPresenter)
    {
        _craftingService = craftingService;
        _view = view;
        _dragDropPresenter = dragPresenter;
        _tooltipPresenter = tooltipPresenter;
        InitCraftingSlots();

        _view.CraftButtonClicked += View_OnCraftButtonClicked;
    }

    private void InitCraftingSlots()
    {
        _slots = new Dictionary<SlotFrameView, ItemStack>();
        _slotsPresenter = new Dictionary<SlotFrameView, SlotFramePresenter>();

        for (int i = 0; i < _view.Slots.Length; i++)
        {
            SlotFrameView slotFrame = _view.Slots[i];
            ItemStack itemSlot = _craftingService.CraftingModel.Items[i];
            UISlot newSlot = new UISlot(i, _craftingService.CraftingModel);
            SlotFramePresenter presenter = new SlotFramePresenter(newSlot, slotFrame, _tooltipPresenter, _dragDropPresenter);

            _slots.Add(slotFrame, itemSlot);
            _slotsPresenter.Add(slotFrame, presenter);

            itemSlot.Changed += ItemSlot_Changed;
        }

        _craftingSlotPresenter = new SlotFramePresenter(new UISlot(0, _craftingService.Item), _view.CraftingSlot, _tooltipPresenter, _dragDropPresenter);
        _craftingSlotPresenter.SetStatus(false);
    }

    private void ItemSlot_Changed(ItemStack obj)
    {
        ItemStack itemStack = _craftingService.TryCraft();

        if (itemStack == null)
        {
            return;
        }

        ShotPreview(itemStack);
    }

    private void View_OnCraftButtonClicked()
    {
        _craftingSlotPresenter.SetStatus(true);
    }

    private void ShotPreview(ItemStack item)
    {
        _craftingSlotPresenter.SetItem(item);
    }
}