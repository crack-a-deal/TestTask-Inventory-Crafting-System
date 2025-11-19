public class SlotFramePresenterFactory
{
    private readonly DragAndDropController _dragDropPresenter;
    private readonly TooltipPresenter _tooltipPresenter;

    public SlotFramePresenterFactory(DragAndDropController dragPresenter, TooltipPresenter tooltipPresenter)
    {
        _dragDropPresenter = dragPresenter;
        _tooltipPresenter = tooltipPresenter;
    }

    public SlotFramePresenter Create(UISlot slot, SlotFrameView view)
    {
        return new SlotFramePresenter(slot, view, _tooltipPresenter, _dragDropPresenter);
    }
}