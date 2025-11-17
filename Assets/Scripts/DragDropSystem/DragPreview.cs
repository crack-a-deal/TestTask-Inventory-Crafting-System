using UnityEngine;
using UnityEngine.UI;

public class DragPreview : MonoBehaviour
{
    [SerializeField] private Image preview;

    public void Show(Sprite icon)
    {
        preview.sprite = icon;
        preview.enabled = true;
    }
    public void Hide()
    {
        preview.enabled = false;
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }
}
