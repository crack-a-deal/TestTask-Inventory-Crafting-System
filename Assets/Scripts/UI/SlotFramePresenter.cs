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

    private bool _canTake = false;

    public bool CanTake => _canTake;

    public ItemStack Slot
    {
        get
        {
            return _slot;
        }
        set
        {
            if (_slot == value)
            {
                return;
            }

            _slot = value;
        }
    }


    public SlotFramePresenter(UISlot slot, SlotFrameView view, TooltipPresenter tooltip, DragAndDropController dragDropPresenter)
    {
        _uiSlot = slot;

        _slot = _uiSlot.Container.GetItem(_uiSlot.Index);
        _view = view;
        _tooltip = tooltip;
        _dragDropPresenter = dragDropPresenter;

        UpdateSlotView(_slot);

        _slot.Changed += InventoryItem_OnChanged;

        _view.ItemEnter += View_OnItemEnter;
        _view.ItemMove += View_OnItemMove;
        _view.ItemExit += View_OnItemExit;

        _view.DragedItem.BeginDrag += DragedItem_BeginDrag;
        _view.DragedItem.Drag += DragedItem_Drag;
        _view.DragedItem.EndDrag += DragedItem_EndDrag;

        _view.ItemDropped += View_ItemDropped;

        _canTake = true;
    }


    private void View_ItemDropped(SlotFrameView slotView)
    {
        _dragDropPresenter.EndDrag(_uiSlot, slotView.transform.position);
    }

    private void DragedItem_EndDrag(Vector2 position)
    {
        _dragDropPresenter.EndDrag(null, position);
        _view.ShowItem(true);
        UpdateSlotView(_slot);
    }

    private void DragedItem_Drag(Vector2 position)
    {
        _dragDropPresenter.Drag(position);
    }

    private void DragedItem_BeginDrag(Vector2 position)
    {
        if (_canTake == false)
        {
            return;
        }

        bool isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);

        _view.ShowItem(false);

        _dragDropPresenter.BeginDrag(_uiSlot, isShift);
    }

    private void View_OnItemEnter()
    {
        if (_slot.Item == null)
        {
            return;
        }

        _tooltip.SetItem(_slot);
        _tooltip.Show();
    }

    private void View_OnItemMove(Vector2 obj)
    {
        _tooltip.Move(obj);
    }

    private void View_OnItemExit()
    {
        _tooltip.Hide();
    }



    public void SetItem(ItemStack item)
    {
        _slot = item;
        UpdateSlotView(_slot);
    }

    public void SetTakable(bool canTake)
    {
        _canTake = canTake;
    }

    private void InventoryItem_OnChanged(ItemStack stack)
    {
        UpdateSlotView(stack);
    }

    private void UpdateSlotView(ItemStack stack)
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