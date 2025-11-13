using UnityEngine;

public class SlotFramePresenter
{
    private readonly InventoryItem _slot;
    private readonly SlotFrameView _view;

    public SlotFramePresenter(InventoryItem slot, SlotFrameView view)
    {
        _slot = slot;
        _view = view;

        UpdateSlotView(_slot);

        _slot.Changed += InventoryItem_OnChanged;
    }

    private void InventoryItem_OnChanged(InventoryItem item)
    {
        UpdateSlotView(item);
    }

    private void UpdateSlotView(InventoryItem item)
    {
        if (item.Item == null || item.Count == 0)
        {
            _view.ShowItem(false);
            return;
        }
        else
        {
            _view.ShowItem(true);
        }

        _view.SetIcon(item.Item.Icon);
        _view.SetCount(item.Count.ToString());
    }
}