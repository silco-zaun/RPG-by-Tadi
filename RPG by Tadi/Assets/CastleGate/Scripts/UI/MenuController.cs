using Tadi.UI.ScrollView;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Text upText;
    [SerializeField] private Text downText;

    private ScrollViewItemPool itemPool;
    private ContentInfo curContentInfo;

    // UI Info
    private float viewPortWidth;
    private float viewPortHeight;
    private float itemWidth;
    private float itemHeight;
    private int itemCountPerPage;

    //public event System.UnitAction<int> onSubmit;

    public ContentInfoTree Tree { get; set; }

    private void Awake()
    {
        itemPool = GetComponent<ScrollViewItemPool>();

        Rect viewportRect = scrollRect.viewport.rect;
        Rect itemRect = itemPool.ItemRect;

        viewPortWidth = viewportRect.width;
        viewPortHeight = viewportRect.height;
        itemWidth = viewPortWidth;
        itemHeight = itemRect.height;
        itemCountPerPage = (int)(viewPortHeight / itemHeight);

        Tree = new ContentInfoTree(itemCountPerPage);
    }

    public int ChangeMenu(MenuType type, int depthIndex, int menuIndex)
    {
        ClearMenus();

        curContentInfo = Tree.GetContentInfo(type, depthIndex, menuIndex);

        foreach (ItemInfo itemInfo in curContentInfo.ItemInfoList)
        {
            itemPool.ActivateItem(itemInfo);
        }

        SetUpAndDownText(IsOverflow());
        int index = itemPool.GetInitIndex(curContentInfo.CurItemIndex);
        curContentInfo.PrevItemIndex = curContentInfo.CurItemIndex;
        curContentInfo.CurItemIndex = index;

        SelectItem();

        return index;
    }

    public bool IsOverflow()
    {
        bool isOverflow = curContentInfo.ItemInfoList.Count > curContentInfo.ItemCountPerPage;

        return isOverflow;
    }

    public void ClearMenus()
    {
        itemPool.DeactivateAllItem();
        SetUpAndDownText(false);
    }

    public void SetItemState(MenuType type, int depthIndex, int menuIndex, int unitIndex, ItemState colorState)
    {
        ContentInfo menuInfo = Tree.GetContentInfo(type, depthIndex, menuIndex);

        menuInfo.ItemInfoList[unitIndex].ColorState = colorState;
    }

    public void SelectItem()
    {
        itemPool.SetItemsColor(curContentInfo.CurItemIndex);

        if (curContentInfo.IsChangedPage)
            ChangePage(curContentInfo.CurItemPage, curContentInfo.ItemCountPerPage);
    }

    public int SelectItem(Vector2 vector)
    {
        ContentInfo menuInfo = curContentInfo;

        menuInfo.PrevItemIndex = menuInfo.CurItemIndex;

        if (vector.y > 0)
            menuInfo.CurItemIndex--;
        else if (vector.y < 0)
            menuInfo.CurItemIndex++;

        menuInfo.CurItemIndex = (menuInfo.CurItemIndex + itemPool.ItemsCount) % itemPool.ItemsCount;

        itemPool.SetItemsColor(menuInfo.CurItemIndex);

        if (menuInfo.IsChangedPage)
            ChangePage(menuInfo.CurItemPage, menuInfo.ItemCountPerPage);

        return menuInfo.CurItemIndex;
    }

    private void SetUpAndDownText(bool activate)
    {
        upText.gameObject.SetActive(activate);
        downText.gameObject.SetActive(activate);
    }

    private void ChangePage(int curItemPage, int itemCountPerPage)
    {
        int firstItemIndexInPage = (curItemPage - 1) * itemCountPerPage;

        ScrollToTargetItem(firstItemIndexInPage);
    }

    private void ScrollToTargetItem(int targetItemIndex)
    {
        float viewPortWidth = this.viewPortWidth;
        float viewPortHeight = this.viewPortHeight;
        float itemWidth = this.itemWidth;
        float itemHeight = this.itemHeight;

        //Canvas.ForceUpdateCanvases();
        Vector2 viewPortLocalPosition = scrollRect.viewport.localPosition;
        Vector2 targetItemLocalPosition = itemPool.GetItemLocalPostion(targetItemIndex);

        Vector2 newTargetLocalPosition = new Vector2(
            0 - (viewPortLocalPosition.x + targetItemLocalPosition.x) + (viewPortWidth / 2) - (itemWidth / 2),
            0 - (viewPortLocalPosition.y + targetItemLocalPosition.y) + (viewPortHeight / 2) - (itemHeight / 2));

        scrollRect.content.localPosition = newTargetLocalPosition;
    }

    public int SubmitItem()
    {
        int curItemIndex = curContentInfo.CurItemIndex;

        return curItemIndex;
    }
}
