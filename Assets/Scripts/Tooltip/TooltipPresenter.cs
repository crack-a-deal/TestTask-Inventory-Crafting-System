using UnityEngine;

public class TooltipPresenter
{
    private ItemStack _item;
    private readonly TooltipView _view;

    public TooltipPresenter(TooltipView view)
    {
        _view = view;
    }

    public void SetItem(ItemStack item)
    {
        if(item.Item == null)
        {
            return;
        }

        _item = item;
        _view.SetTooltip(_item.Item.Title, _item.Item.Title, _item.Count.ToString());
    }

    public void Show()
    {
        _view.Show();
    }

    public void Move(Vector2 position)
    {
        _view.Move(position);
    }

    public void Hide()
    {
        _view.Hide();
    }
}