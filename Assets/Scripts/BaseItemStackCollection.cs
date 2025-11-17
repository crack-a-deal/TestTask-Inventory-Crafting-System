using UnityEngine;

[System.Serializable]
public abstract class BaseItemStackCollection
{
    [SerializeField] private ItemStack[] _slots;

    public ItemStack[] Items => _slots;

    public int Size => _slots.Length;

    public BaseItemStackCollection(int size)
    {
        _slots = new ItemStack[size];
        for (int i = 0; i < size; i++)
        {
            _slots[i] = new ItemStack();
        }
    }

    public ItemStack GetItem(int index)
    {
        return _slots[index];
    }

    public void SetItem(int index, ItemStack item)
    {
        _slots[index] = item;
    }
}
