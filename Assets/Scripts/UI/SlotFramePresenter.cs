public class SlotFramePresenter
{
    private readonly ItemSlot _slot;
    private readonly SlotFrameView _view;

    public SlotFramePresenter(ItemSlot slot, SlotFrameView view)
    {
        _slot = slot;
        _view = view;

        //_view.SetItem(_slot.Item.Icon, _slot.Count.ToString());
        _slot.CountChanged += Slot_OnCountChanged;
    }

    private void Slot_OnCountChanged(int count)
    {
        _view.SetItem(_slot.Item.Icon, _slot.Count.ToString());
    }
}