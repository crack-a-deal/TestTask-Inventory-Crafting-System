using UnityEngine;

public class UISlot
{
    public UISlot(int index, IItemContainer container)
    {
        Index = index;
        Container = container;
    }

    public int Index { get; set; }
    public IItemContainer Container { get; set; }
}


public class SlotFramePresenter
{
    private UISlot _uiSlot;
    private ItemStack _slot;
    private readonly SlotFrameView _view;

    private readonly TooltipPresenter _tooltip;
    private readonly DragAndDropController _dragDropPresenter;


    public SlotFramePresenter(UISlot slot, SlotFrameView view, TooltipPresenter tooltip, DragAndDropController dragDropPresenter)
    {
        _uiSlot = slot;

        _slot = _uiSlot.Container.GetItem(_uiSlot.Index);
        _view = view;
        _tooltip = tooltip;
        _dragDropPresenter = dragDropPresenter;

        UpdateSlotView(_slot);

        _slot.Changed += InventoryItem_OnChanged;

        _view.ItemEnter += _view_ItemEnter;
        _view.ItemMove += _view_ItemMove;
        _view.ItemExit += _view_ItemExit;

        _view.DragedItem.BeginDrag += DragedItem_BeginDrag;
        _view.DragedItem.Drag += DragedItem_Drag;
        _view.DragedItem.EndDrag += DragedItem_EndDrag;

        _view.ItemDropped += View_ItemDropped;
    }

    private void View_ItemDropped(SlotFrameView obj)
    {
        _dragDropPresenter.EndDrag(_uiSlot);
        //_dragDropPresenter.SetTargetSlot(obj);
    }

    private void DragedItem_EndDrag(Vector2 obj)
    {
        _dragDropPresenter.EndDrag(null);
        _view.ShowItem(true);
        UpdateSlotView(_slot);
    }

    private void DragedItem_Drag(Vector2 position)
    {
        _dragDropPresenter.Drag(position);
    }

    private void DragedItem_BeginDrag(Vector2 position)
    {
        _view.ShowItem(false);
        _dragDropPresenter.BeginDrag(_uiSlot);
    }

    private void _view_ItemMove(Vector2 obj)
    {
        _tooltip.Move(obj);
    }

    private void _view_ItemExit()
    {
        _tooltip.Hide();
    }

    private void _view_ItemEnter()
    {
        if (_slot.Item == null)
        {
            return;
        }

        _tooltip.SetItem(_slot);
        _tooltip.Show();
    }

    public void SetItem(ItemStack item)
    {
        _slot = item;
        UpdateSlotView(_slot);
    }

    private void InventoryItem_OnChanged(ItemStack stack)
    {
        UpdateSlotView(stack);
    }

    public void UpdateSlotView(ItemStack stack)
    {
        if (stack.Item == null || stack.Count == 0)
        {
            _view.ShowItem(false);
            return;
        }
        else
        {
            _view.ShowItem(true);
        }

        _view.SetIcon(stack.Item.Icon);
        _view.SetCount(stack.Count.ToString());
    }
}