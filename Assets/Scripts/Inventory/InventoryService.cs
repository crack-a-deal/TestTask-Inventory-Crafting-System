using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryService
{
    private Inventory _inventory;
    private readonly ItemDatabase _itemDatabase;

    public Inventory Inventory => _inventory;

    public InventoryService(ItemDatabase itemDatabase)
    {
        _itemDatabase = itemDatabase;
        _inventory = new Inventory(24);
    }

    public void Fill(ItemData[] itemData)
    {
        if (itemData == null || itemData.Length == 0)
        {
            return;
        }

        for (int i = 0; i < _inventory.Items.Length / 2; i++)
        {
            ItemData randomItem = itemData[Random.Range(0, itemData.Length)];

            int count = Random.Range(1, 99);

            var slot = _inventory.GetItem(i);

            slot.Item = randomItem;
            slot.Count = count;
        }
    }

    public void Clear()
    {
        foreach (var item in _inventory.Items)
        {
            item.Item = null;
            item.Count = 0;
        }
    }

    public void MoveItem(int fromIndex, int toIndex)
    {
        if (fromIndex == toIndex)
        {
            return;
        }

        InventoryItem fromItem = _inventory.GetItem(fromIndex);
        InventoryItem toItem = _inventory.GetItem(toIndex);

        if (toItem.Item == null)
        {
            toItem.Item = fromItem.Item;
            toItem.Count = fromItem.Count;

            fromItem.Item = null;
            fromItem.Count = 0;
            return;
        }

        if(fromItem.Item == toItem.Item && fromItem.Item.IsStackable)
        {
            toItem.Count += fromItem.Count;

            fromItem.Item = null;
            fromItem.Count = 0;
            return;
        }

        SwapItems(fromItem, toItem);
    }

    private void SwapItems(InventoryItem fromItem,InventoryItem toItem)
    {
        InventoryItem it = new InventoryItem();
        it.Item = fromItem.Item;
        it.Count = fromItem.Count;

        fromItem.Item = toItem.Item;
        fromItem.Count = toItem.Count;

        toItem.Item = it.Item;
        toItem.Count = it.Count;
    }

    public int GetItemIndex(InventoryItem item)
    {
        return Array.IndexOf(_inventory.Items, item);
    }
}

