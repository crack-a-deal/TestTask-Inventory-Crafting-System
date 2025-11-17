using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotFrameView : MonoBehaviour, IPointerEnterHandler, IPointerMoveHandler, IPointerExitHandler, IDropHandler
{
    public event Action<SlotFrameView> ItemDropped;

    public event Action ItemEnter;
    public event Action<Vector2> ItemMove;
    public event Action ItemExit;

    [SerializeField] private DragItemView dragItemView;
    [SerializeField] private TMP_Text itemCount;

    [SerializeField] private Image targetGraphic;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite selectedSprite;


    public DragItemView DragedItem => dragItemView; 

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetGraphic.sprite = selectedSprite;
        ItemEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetGraphic.sprite = defaultSprite;
        ItemExit?.Invoke();
    }
    public void OnDrop(PointerEventData eventData)
    {
        ItemDropped?.Invoke(this);
    }

    public void SetIcon(Sprite icon)
    {
        dragItemView.Icon.sprite = icon;
    }

    public void SetCount(string count)
    {
        itemCount.text = count;
    }

    public void ShowItem(bool isShow)
    {
        dragItemView.Icon.enabled = isShow;
        itemCount.enabled = isShow;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        ItemMove?.Invoke(eventData.position);
    }
}
