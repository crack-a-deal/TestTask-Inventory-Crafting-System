using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItemView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public event Action<Vector2> BeginDrag;
    public event Action<Vector2> Drag;
    public event Action<Vector2> EndDrag;

    [SerializeField] private Image image;

    public Image Icon => image;

    public void OnBeginDrag(PointerEventData eventData)
    {
        //image.enabled = false;
        BeginDrag?.Invoke(eventData.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Drag?.Invoke(eventData.position);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //image.enabled = true;
        EndDrag?.Invoke(eventData.position);
    }
}

