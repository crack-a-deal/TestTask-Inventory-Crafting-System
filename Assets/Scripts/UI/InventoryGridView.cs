using System;
using UnityEngine;

public class InventoryGridView : MonoBehaviour
{
    public event Action<SlotFrameView, DragData> ItemBeginDrag;
    public event Action<DragData> ItemDrag;
    public event Action<DragData> ItemEndDrag;
    public event Action<SlotFrameView> ItemDropped;

    [SerializeField] private SlotFrameView[] slots;
    public SlotFrameView[] Slots => slots;

    private void Awake()
    {
        foreach (var item in slots)
        {
            item.ItemBeginDrag += Item_ItemBeginDrag;
            item.ItemDrag += x => ItemDrag?.Invoke(x);
            item.ItemEndDrag += x => ItemEndDrag?.Invoke(x);
            item.ItemDropped += x=> ItemDropped?.Invoke(x);
        }
    }

    private void Item_ItemBeginDrag(SlotFrameView arg1, DragData arg2)
    {
        ItemBeginDrag?.Invoke(arg1, arg2);
    }
}
