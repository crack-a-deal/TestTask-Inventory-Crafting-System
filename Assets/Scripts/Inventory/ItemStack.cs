using System;
using UnityEngine;

[System.Serializable]
public class ItemStack
{
    public event Action<ItemStack> Changed;

    [SerializeField]  private ItemData _item;
    public ItemData Item
    {
        get => _item;
        set
        {
            if (_item == value)
            {
                return;
            }
            _item = value;
            Changed?.Invoke(this);
        }
    }

    [SerializeField] private int _count;

    public ItemStack() { }

    public ItemStack(ItemData item, int count)
    {
        Item = item;
        Count = count;
    }

    public int Count
    {
        get => _count;
        set
        {
            if (_count == value)
            {
                return;
            }
            _count = value;
            Changed?.Invoke(this);
        }
    }
}
