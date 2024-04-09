using Tadi.Datas.UI;
using Tadi.UI.ScrollView;
using UnityEngine;
using UnityEngine.UI;
using static Tadi.UI.ScrollView.ItemInfo;

public class ScrollViewItemController : MonoBehaviour
{
    private Text text;

    public ItemInfo ItemInfo { get; private set; }

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    public void Init(ItemInfo itemInfo)
    {
        ItemInfo = itemInfo;
        text.text = itemInfo.Name;

        SetUnselectedItemColor();
    }

    public void SetUnselectedItemColor()
    {
        switch (ItemInfo.ColorState)
        {
            case ItemState.Origin:
                text.color = UIData.OriginColor;
                break;
            case ItemState.Deactivated:
                text.color = UIData.DeactivateColor;
                break;
            default:
                break;
        }
    }

    public void SetSelectedItemColor()
    {
        GetComponent<Text>().color = UIData.SelectedColor;
    }
}
