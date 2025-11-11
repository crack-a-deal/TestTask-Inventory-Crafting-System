using UnityEngine;

public class InventoryGridView : MonoBehaviour
{
    [SerializeField] private SlotFrameView[] slots;
    public SlotFrameView[] Slots => slots;

}
