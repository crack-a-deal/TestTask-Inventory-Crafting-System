using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/RecipeDatabase")]
public class RecipeDatabase : ScriptableObject
{
    [SerializeField] private Recipe[] recipes;

    public Recipe[] GetAllRecipes()
    {
        return recipes;
    }
}