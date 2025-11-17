using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryService
{
    private InventoryCollection _inventory;
    private readonly ItemDatabase _itemDatabase;

    public InventoryCollection Inventory => _inventory;

    public InventoryService(ItemDatabase itemDatabase)
    {
        _itemDatabase = itemDatabase;
        _inventory = new InventoryCollection(24);
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

            int count = Random.Range(1, randomItem.MaxStack);

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

    public void MoveItem(ItemStack fromItem, ItemStack toItem)
    {
        if (fromItem == toItem)
        {
            return;
        }


        if (toItem.Item == null)
        {
            toItem.Item = fromItem.Item;
            toItem.Count = fromItem.Count;

            fromItem.Item = null;
            fromItem.Count = 0;

            Debug.Log($"Move {fromItem.Item?.Title} ({fromItem.Count}) || {toItem.Item?.Title} ({toItem.Count})");
            return;
        }

        if (fromItem.Item == toItem.Item && fromItem.Item.IsStackable)
        {
            toItem.Count += fromItem.Count;

            fromItem.Item = null;
            fromItem.Count = 0;
            return;
        }

        SwapItems(fromItem, toItem);
    }

    private void SwapItems(ItemStack fromItem, ItemStack toItem)
    {
        ItemStack it = new ItemStack();
        it.Item = fromItem.Item;
        it.Count = fromItem.Count;

        fromItem.Item = toItem.Item;
        fromItem.Count = toItem.Count;

        toItem.Item = it.Item;
        toItem.Count = it.Count;
    }

    public void RemoveItem(int index)
    {
        ItemStack inventoryItem = _inventory.GetItem(index);
        inventoryItem.Item = null;
        inventoryItem.Count = 0;
    }

    public int GetItemIndex(ItemStack item)
    {
        return Array.IndexOf(_inventory.Items, item);
    }
}

