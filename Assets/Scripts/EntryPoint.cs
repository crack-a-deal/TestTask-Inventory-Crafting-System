using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [Header("Services")]
    [SerializeField] private ItemDatabase itemsData;
    [SerializeField] private RecipeDatabase recipeDatabase;

    [Header("UI")]
    [SerializeField] private InventoryPageView inventoryPage;
    [SerializeField] private CraftingPageView craftingPage;

    [SerializeField] private DragPreview draggedItemPreview;
    [SerializeField] private TooltipView tooltip;

    private InventoryService _inventoryService;
    private CraftingService _craftingService;

    private InventoryPagePresenter _inventoryPresenter;
    private CraftingPagePresenter _craftingPresenter;

    private DragAndDropController _dragAndDropController;
    private TooltipPresenter _tooltipPresenter;

    private void Awake()
    {
        InitializeServices();
        InitializeUI();
    }

    private void InitializeServices()
    {
        _inventoryService = new InventoryService(itemsData);
        _craftingService = new CraftingService(recipeDatabase);
    }

    private void InitializeUI()
    {
        _dragAndDropController = new DragAndDropController(draggedItemPreview);
        _tooltipPresenter = new TooltipPresenter(tooltip);

        _inventoryPresenter = new InventoryPagePresenter(_inventoryService, inventoryPage, _dragAndDropController, _tooltipPresenter);
        _craftingPresenter = new CraftingPagePresenter(_craftingService, craftingPage, _dragAndDropController, _tooltipPresenter);
    }
}
