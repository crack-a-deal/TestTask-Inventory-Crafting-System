using System;
using UnityEngine;

public class DragAndDropController
{
    private readonly DragPreview _preview;


    private ItemStack _draggedItem;
    private IItemContainer _sourceSlot;
    private IItemContainer _targetSlot;

    private UISlot _fromSlot;


    public DragAndDropController(DragPreview preview)
    {
        _preview = preview;
    }

    public void BeginDrag(UISlot slot)
    {
        _draggedItem = slot.Container.GetItem(slot.Index);
        _sourceSlot = slot.Container;
        _fromSlot = slot;

        _preview.Show(_draggedItem.Item.Icon);
    }

    public void Drag(Vector2 position)
    {
        _preview.SetPosition(position);
    }

    public void EndDrag(UISlot slot)
    {
        _preview.Hide();

        if (slot == null)
        {
            return;
        }

        _targetSlot = slot.Container;
        MoveItem(_fromSlot, slot);
    }

    private void MoveItem(UISlot from, UISlot to)
    {
        var fromStack = from.Container.GetItem(from.Index);
        var toStack = to.Container.GetItem(to.Index);

        Debug.Log($"Move {fromStack.Item?.Title} ({fromStack.Count}) || {toStack.Item?.Title} ({toStack.Count})");

        if (toStack.Item == null)
        {
            toStack.Item = fromStack.Item;
            toStack.Count = fromStack.Count;

            fromStack.Item = null;
            fromStack.Count = 0;

            return;
        }

        if (fromStack.Item == toStack.Item && fromStack.Item.IsStackable)
        {
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
