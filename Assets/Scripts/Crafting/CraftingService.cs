using System.Linq;

public class CraftingService
{
    private CraftCollection _craftingModel;
    private CraftItem _item;

    private readonly RecipeDatabase _recipes;

    public CraftCollection CraftingModel => _craftingModel;
    public CraftItem Item => _item;

    public CraftingService(RecipeDatabase recipes)
    {
        _recipes = recipes;
        _craftingModel = new CraftCollection(9);
        _item = new CraftItem(1);

        for (int i = 0; i < 9; i++)
        {
            _craftingModel.Items[i] = new ItemStack(null, 0);
        }

    }

    public ItemStack TryCraft()
    {
        foreach (Recipe recipe in _recipes.GetAllRecipes())
        {
            if (CheckRecipe(recipe))
            {
                _item.Items[0] = new ItemStack(recipe.ResultItem, recipe.ResultCount);
                return _item.GetItem(0);
            }
        }
        return null;
    }

    private bool CheckRecipe(Recipe recipe)
    {
        int v = _craftingModel.Items.Count(i => i.Item != null);

        if (v != recipe.Ingredients.Length)
        {
            return false;
        }

        foreach (var ingredient in recipe.Ingredients)
        {
            int availableCount = 0;
            foreach (var item in _craftingModel.Items)
            {
                if (ingredient.Item == item.Item)
                {
                    availableCount += item.Count;
                }

                if (availableCount >= ingredient.Count)
                {
                    break;
                }
            }

            if (availableCount < ingredient.Count)
            {
                return false;
            }
        }

        return true;
    }
}
