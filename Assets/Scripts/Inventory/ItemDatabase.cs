using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemDatabase")]
public class ItemDatabase : ScriptableObject
{
    [SerializeField] private ItemData[] items;

    public ItemData[] GetAllItems()
    {
        return items;
    }
}
