using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/ItemData")]
public class ItemData : ScriptableObject
{
    [SerializeField] private ItemType type;
    [SerializeField] private string title;
    [SerializeField] private Sprite icon;
    [SerializeField] private int maxStack = 99;
    [SerializeField] private bool isStackable;

    public string Title => title;
    public Sprite Icon => icon;

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(title))
        {
            return;
        }

        title = name;
    }
}
