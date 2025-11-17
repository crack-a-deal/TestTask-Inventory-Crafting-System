using UnityEngine;
using UnityEngine.UI;

public class DragPreview : MonoBehaviour
{
    [SerializeField] private Image preview;

    public void Show(Sprite icon, Vector2 startPosition)
    {
        preview.sprite = icon;
        transform.position = startPosition;
        preview.enabled = true;
    }

    public void Move(Vector2 position)
    {
        transform.position= position;
    }

    public void Hide()
    {
        preview.enabled = false;
    }
}
