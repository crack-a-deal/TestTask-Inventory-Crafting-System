using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [Header("Services")]
    [SerializeField] private ItemDatabase itemsData;
    [SerializeField] private RecipeDatabase recipeDatabase;

    [Header("UI")]
    [SerializeField] private DragPreview draggedItemPreview;
    [SerializeField] private RectTransform droppedArea;
    [SerializeField] private TooltipView tooltip;

    [SerializeField] private InventoryPageView inventoryPage;
    [SerializeField] private CraftingPageView craftingPage;

    [Header("Testing")]
    [SerializeField] private NewBehaviourScript behaviourScript;

    private InventoryService _inventoryService;
    private CraftingService _craftingService;

    private DragAndDropController _dragAndDropController;
    private TooltipPresenter _tooltipPresenter;

    private SlotFramePresenterFactory _slotFramePresenterFactory;

    private InventoryPagePresenter _inventoryPresenter;
    private CraftingPagePresenter _craftingPresenter;

    private void Awake()
    {
        InitializeServices();
        InitializeUI();

        behaviourScript.craftingService = _craftingService;
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

        _slotFramePresenterFactory = new SlotFramePresenterFactory(_dragAndDropController, _tooltipPresenter);

        _inventoryPresenter = new InventoryPagePresenter(_inventoryService, inventoryPage,_dragAndDropController, _slotFramePresenterFactory);
        _craftingPresenter = new CraftingPagePresenter(_craftingService, craftingPage, _slotFramePresenterFactory);
    }
}
