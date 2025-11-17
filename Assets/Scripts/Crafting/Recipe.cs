using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Recipe")]

public class Recipe : ScriptableObject
{
    [SerializeField] private ItemStack[] ingredients;
    [SerializeField] private ItemData resultItem;
    [SerializeField] private int resultCount;

    public ItemStack[] Ingredients => ingredients;
    public ItemData ResultItem => resultItem;
    public int ResultCount => resultCount;
}