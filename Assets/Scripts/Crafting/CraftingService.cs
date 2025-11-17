using System.Collections.Generic;
using UnityEngine;

public class CraftingService
{
    private CraftCollection _craftingModel;

    public CraftCollection CraftingModel => _craftingModel;

    public CraftingService()
    {
        _craftingModel = new CraftCollection(9);

        for (int i = 0; i < 9; i++)
        {
            _craftingModel.Items[i] = new ItemStack(null, 0);
        }
    }

    //public Recipe MatchRecipe(List<ItemStack> items)
    //{
    //    foreach (var recipe in recipes)
    //    {
    //        if (Matches(items, recipe))
    //        {
    //            return recipe;
    //        }
    //    }
    //    return null;
    //}

    private bool Matches(List<ItemStack> items, Recipe recipe)
    {
        if (items.Count != recipe.Ingredients.Count)
            return false;

        var pool = new List<ItemStack>(recipe.Ingredients);


        foreach (var stack in items)
        {
            if (!pool.Contains(stack))
                return false;

            pool.Remove(stack);
        }

        return true;
    }
}
