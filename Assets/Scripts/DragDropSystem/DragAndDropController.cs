using System;
using UnityEngine;

public class DragAndDropController
{
    public event Action<UISlot,Vector2> ItemDropped;

    private readonly DragPreview _preview;

    private ItemStack _draggedItem;
    private IItemContainer _sourceSlot;
    private IItemContainer _targetSlot;

    private UISlot _fromSlot;

    public DragAndDropController(DragPreview preview)
    {
        _preview = preview;
        _preview.Hide();
    }

    public void BeginDrag(UISlot slot, bool isShift = false)
    {
        _draggedItem = slot.Container.GetItem(slot.Index);
        _sourceSlot = slot.Container;
        _fromSlot = slot;


        if (isShift)
        {
            int draggedCount = _draggedItem.Count / 2;

            if (draggedCount <= 0)
            {
                return;
            }


            slot.Container.GetItem(slot.Index).Count -= draggedCount;
            _draggedItem = new ItemStack(_draggedItem.Item, draggedCount);
        }

        _preview.Show(_draggedItem.Item.Icon, _draggedItem.Count.ToString());
    }

    public void Drag(Vector2 position)
    {
        _preview.SetPosition(position);
    }

    public void EndDrag(UISlot slot, Vector2 position)
    {
        _preview.Hide();

        if (slot == null)
        {
            ItemDropped?.Invoke(_fromSlot, position);
            return;
        }

        _targetSlot = slot.Container;
        MoveItem(_fromSlot, slot);
    }

    private void MoveItem(UISlot from, UISlot to)
    {
        var fromStack = _draggedItem;
        var toStack = to.Container.GetItem(to.Index);

        // Move
        if (toStack.Item == null)
        {
            toStack.Item = fromStack.Item;
            toStack.Count = fromStack.Count;

            fromStack.Item = null;
            fromStack.Count = 0;

            return;
        }

        // Stack
        if (fromStack.Item == toStack.Item && fromStack.Item.IsStackable)
        {
            if (toStack.Count + fromStack.Count > toStack.Item.MaxStack)
            {
                return;
            }

            toStack.Count += fromStack.Count;

            fromStack.Item = null;
            fromStack.Count = 0;
            return;
        }

        SwapItems(fromStack, toStack);
    }

    private void SwapItems(ItemStack fromItem, ItemStack toItem)
    {
        ItemStack it = new ItemStack();
        it.Item = fromItem.Item;
        it.Count = fromItem.Count;

        fromItem.Item = toItem.Item;
        fromItem.Count = toItem.Count;

        toItem.Item = it.Item;
        toItem.Count = it.Count;
    }
}
