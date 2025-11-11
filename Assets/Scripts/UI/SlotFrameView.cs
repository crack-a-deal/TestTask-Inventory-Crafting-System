using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotFrameView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemCount;

    [SerializeField] private Image targetGraphic;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private Sprite selectedSprite;

    public void OnPointerEnter(PointerEventData eventData)
    {
        targetGraphic.sprite = selectedSprite;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        targetGraphic.sprite = defaultSprite;
    }

    public void SetItem(Sprite icon, string count)
    {
        itemIcon.enabled = true;
        itemCount.enabled = true;

        itemIcon.sprite = icon;
        itemCount.text = count;
    }
}
