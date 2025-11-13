using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropPresenter : MonoBehaviour
{
    [SerializeField] private DragPreview preview;

    public void BeginDrag(Sprite icon)
    {
        preview.Show(icon,Input.mousePosition);
    }

    public void Drag(Vector2 position)
    {
        preview.Move(position);
    }

    public void EndDrag()
    {
        preview.Hide();
    }
}
