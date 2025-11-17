using System;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPageView : MonoBehaviour
{
    public event Action AllTypeButtonClicked;
    public event Action ResourceButtonClicked;
    public event Action ToolButtonClicked;

    public event Action FillButtonClicked;
    public event Action ClearButtonClicked;

    [Header("Inventory")]
    [SerializeField] private SlotFrameView[] slots;
    [SerializeField] private RectTransform inventoryArea;

    [Header("Tabs")]
    [SerializeField] private Button allTypeButton;
    [SerializeField] private Button resourceButton;
    [SerializeField] private Button toolButton;

    [Header("Buttons")]
    [SerializeField] private Button fillButton;
    [SerializeField] private Button clearButton;

    public SlotFrameView[] Slots => slots;
    public RectTransform RectTransform => inventoryArea;

    private void Awake()
    {
        allTypeButton.onClick.AddListener(OnAllTypeButtonClicked);
        resourceButton.onClick.AddListener(OnResourceButtonClicked);
        toolButton.onClick.AddListener(OnToolButtonClicked);

        fillButton.onClick.AddListener(OnFillButtonClicked);
        clearButton.onClick.AddListener(OnClearButtonClicked);
    }

    private void OnDestroy()
    {
        allTypeButton.onClick.RemoveListener(OnAllTypeButtonClicked);
        resourceButton.onClick.RemoveListener(OnResourceButtonClicked);
        toolButton.onClick.RemoveListener(OnToolButtonClicked);

        fillButton.onClick.RemoveListener(OnFillButtonClicked);
        clearButton.onClick.RemoveListener(OnClearButtonClicked);
    }

    private void OnAllTypeButtonClicked()
    {
        AllTypeButtonClicked?.Invoke();
    }

    private void OnResourceButtonClicked()
    {
        ResourceButtonClicked?.Invoke();
    }

    private void OnToolButtonClicked()
    {
        ToolButtonClicked?.Invoke();
    }

    private void OnFillButtonClicked()
    {
        FillButtonClicked?.Invoke();
    }

    private void OnClearButtonClicked()
    {
        ClearButtonClicked?.Invoke();
    }
}
