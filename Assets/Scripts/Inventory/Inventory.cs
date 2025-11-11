public class Inventory
{
    protected class ItemSlot
    {
        public ItemData Item;
        public int Count;
    }

    private ItemSlot[] _slots;

    public Inventory(int size)
    {
        _slots = new ItemSlot[size];
        for (int i = 0; i < size; i++)
        {
            _slots[i] = new ItemSlot();
        }
    }
}