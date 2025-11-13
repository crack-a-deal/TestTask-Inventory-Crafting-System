using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItemView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action<DragData> BeginDrag;
    public event Action<DragData> Drag;
    public event Action<DragData> EndDrag;


    [SerializeField] private Image image;
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.enabled = false;
        BeginDrag?.Invoke(new DragData(image.sprite, eventData.position));
    }

    public void OnDrag(PointerEventData eventData)
    {
        Drag?.Invoke(new DragData(image.sprite, eventData.position));
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        image.enabled = true;
        EndDrag?.Invoke(new DragData(image.sprite, eventData.position));
    }
}

public class DragData
{
    public Sprite Icon;
    public Vector2 Position;

    public DragData(Sprite icon, Vector2 position)
    {
        Icon = icon;
        Position = position;
    }
}
