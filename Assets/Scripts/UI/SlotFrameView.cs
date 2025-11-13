using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotFrameView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDropHandler
{
    public event Action<SlotFrameView,DragData> ItemBeginDrag;
    public event Action<DragData> ItemDrag;
    public event Action<DragData> ItemEndDrag;
    public event Action<SlotFrameView> ItemDropped;

    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemCount;

    [SerializeField] private Image targetGraphic;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite selectedSprite;

    [SerializeField] private DragItemView dragItemView;

    private void Awake()
    {
        dragItemView.BeginDrag += x => ItemBeginDrag?.Invoke(this, x);
        dragItemView.Drag += x => ItemDrag?.Invoke(x);
        dragItemView.EndDrag += x => ItemEndDrag?.Invoke(x);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetGraphic.sprite = selectedSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetGraphic.sprite = defaultSprite;
    }
    public void OnDrop(PointerEventData eventData)
    {
        ItemDropped?.Invoke(this);
    }

    public void SetIcon(Sprite icon)
    {
        itemIcon.sprite = icon;
    }

    public void SetCount(string count)
    {
        itemCount.text = count;
    }

    public void ShowItem(bool isShow)
    {
        dragItemView.enabled = isShow;
        itemIcon.enabled = isShow;
        itemCount.enabled = isShow;
    }
}
