public class InventoryGridPresenter
{
    private readonly Inventory _inventory;
    private readonly InventoryGridView _view;

    public InventoryGridPresenter(Inventory inventory,InventoryGridView view)
    {
        _inventory = inventory;
        _view = view;

        for (int i = 0; i < _view.Slots.Length; i++)
        {
            SlotFrameView slotFrame = _view.Slots[i];
            ItemSlot itemSlot = _inventory.ItemSlots[i];

            SlotFramePresenter presenter = new SlotFramePresenter(itemSlot, slotFrame);
        }
    }
}