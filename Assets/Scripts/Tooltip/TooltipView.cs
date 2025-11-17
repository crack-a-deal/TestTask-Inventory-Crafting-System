using TMPro;
using UnityEngine;

public class TooltipView : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TMP_Text titleLabel;
    [SerializeField] private TMP_Text descriptionLabel;
    [SerializeField] private TMP_Text countLabel;

    public void SetTooltip(string title, string description, string count)
    {
        titleLabel.text = title;
        descriptionLabel.text = description;
        countLabel.text = count;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Move(Vector2 position)
    {
        transform.position = position;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
