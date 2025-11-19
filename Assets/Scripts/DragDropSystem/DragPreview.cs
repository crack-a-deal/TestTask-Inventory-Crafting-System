using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DragPreview : MonoBehaviour
{
    [SerializeField] private Image preview;
    [SerializeField] private TMP_Text countLabel;

    public void Show(Sprite icon, string count)
    {
        gameObject.SetActive(true);

        preview.sprite = icon;
        countLabel.text = count;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetPosition(Vector2 position)
    {
        transform.position = position;
    }
}
