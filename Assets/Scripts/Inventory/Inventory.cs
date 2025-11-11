using System;
using UnityEngine;
using Random = UnityEngine.Random;


[System.Serializable]
public class ItemSlot
{
    public event Action<int> CountChanged;

    public ItemData Item;

    [SerializeField] private int _count;
    public int Count
    {
        get
        {
            return _count;
        }
        set
        {
            if (_count == value)
            {
                return;
            }
            _count = value;
            CountChanged?.Invoke(_count);
        }
    }
}

[System.Serializable]
public class Inventory
{
    [SerializeField] private ItemSlot[] _slots;

    public ItemSlot[] ItemSlots => _slots;

    public Inventory(int size)
    {
        _slots = new ItemSlot[size];
        for (int i = 0; i < size; i++)
        {
            _slots[i] = new ItemSlot();
        }
    }

    public void Fill(ItemData[] itemData)
    {
        if (itemData == null || itemData.Length == 0)
        {
            return;
        }
        for (int i = 0; i < _slots.Length; i++)
        {
            ItemData randomItem = itemData[Random.Range(0, itemData.Length)];

            int count = Random.Range(0, 99);

            _slots[i].Item = randomItem;
            _slots[i].Count = count;
        }
    }

    public void Clear()
    {
        foreach (ItemSlot slot in _slots)
        {
            slot.Item = null;
            slot.Count = 0;
        }
    }
}