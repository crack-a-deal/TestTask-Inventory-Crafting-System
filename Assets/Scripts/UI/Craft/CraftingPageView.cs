using System;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPageView : MonoBehaviour
{
    public event Action<SlotFrameView> ItemDropped;
    public event Action CraftButtonClicked;

    [SerializeField] private SlotFrameView[] slots;
    [SerializeField] private SlotFrameView craftingSlot;
    [SerializeField] private Button craftButton;

    public SlotFrameView[] Slots => slots;


    private void Awake()
    {
        craftButton.onClick.AddListener(OnClicked);
    }


    private void OnClicked()
    {
        CraftButtonClicked?.Invoke();
    }
}
