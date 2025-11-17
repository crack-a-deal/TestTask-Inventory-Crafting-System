using System;
using UnityEngine;


[System.Serializable]
public class InventoryItem
{
    public event Action<InventoryItem> Changed;

    [SerializeField] private ItemData _item;
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

[System.Serializable]
public class Inventory
{
    [SerializeField] private InventoryItem[] _slots;

    public InventoryItem[] Items => _slots;

    public int Size => _slots.Length;

    public Inventory(int size)
    {
        _slots = new InventoryItem[size];
        for (int i = 0; i < size; i++)
        {
            _slots[i] = new InventoryItem();
        }
    }

    public InventoryItem GetItem(int index)
    {
        return _slots[index];
    }

    public void SetItem(int index, InventoryItem item)
    {
        _slots[index] = item;
    }
}