using System.Collections.Generic;
using UnityEngine;

public class InventoryPresenter
{
    private readonly Inventory _inventory;

    private readonly InventoryGridView _view;
    private readonly DragAndDropPresenter dragPresenter;

    private Dictionary<SlotFrameView, InventoryItem> _slots;

    private InventoryItem _currentDragged;


    public InventoryPresenter(Inventory inventory, InventoryGridView view, DragAndDropPresenter dragPresenter)
    {
        _inventory = inventory;
        _view = view;
        this.dragPresenter = dragPresenter;

        _slots = new Dictionary<SlotFrameView, InventoryItem>();
        for (int i = 0; i < _view.Slots.Length; i++)
        {
            SlotFrameView slotFrame = _view.Slots[i];
            InventoryItem itemSlot = _inventory.ItemSlots[i];

            SlotFramePresenter presenter = new SlotFramePresenter(itemSlot, slotFrame);

            _slots.Add(slotFrame, itemSlot);
        }

        _view.ItemBeginDrag += View_OnItemBeginDrag;
        _view.ItemDrag += View_ItemDrag;
        _view.ItemEndDrag += View_ItemEndDrag;
        _view.ItemDropped += View_ItemDropped;
    }

    private void View_ItemDropped(SlotFrameView slot)
    {
        if (!_slots.TryGetValue(slot, out var targetSlot))
        {
            return;
        }

        dragPresenter.EndDrag();
    }


    private void View_ItemEndDrag(DragData obj)
    {
        _currentDragged = null;
        dragPresenter.EndDrag();
    }

    private void View_ItemDrag(DragData dragData)
    {
        dragPresenter.Drag(dragData.Position);
    }

    private void View_OnItemBeginDrag(SlotFrameView slotFrame, DragData dragData)
    {
        if (_slots.TryGetValue(slotFrame, out var item))
        {
            _currentDragged = item;
        }
        dragPresenter.BeginDrag(dragData.Icon);
    }
}