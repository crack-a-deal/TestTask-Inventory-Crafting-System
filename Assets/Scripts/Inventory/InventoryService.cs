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

    public void FillRandom()
    {
        ItemData[] itemDatas = _itemDatabase.GetAllItems();
        for (int i = 0; i < _inventory.Items.Length / 2; i++)
        {
            ItemData randomItem = itemDatas[Random.Range(0, itemDatas.Length)];

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
}

