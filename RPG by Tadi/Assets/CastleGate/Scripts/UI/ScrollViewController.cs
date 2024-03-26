using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using static UnityEditor.Progress;

public class ScrollViewController : MonoBehaviour
{
    public class MenuInfo
    {
        private List<ItemInfo> itemsInfo;
        private int prevItemIndex;
        private int curItemIndex;
        private int prevItemPage;
        private int curItemPage;
        private int itemCountPerPage;

        public List<ItemInfo> ItemsInfos { get { return itemsInfo; } set { itemsInfo = value; } }
        public int PrevItemIndex
        {
            get { return prevItemIndex; }
            set
            {
                prevItemIndex = value;
                prevItemPage = (prevItemIndex / itemCountPerPage) + 1;
            }
        }
        public int CurItemIndex
        {
            get { return curItemIndex; }
            set
            {
                curItemIndex = value;
                curItemPage = (curItemIndex / itemCountPerPage) + 1;
            }
        }
        public int CurItemPage { get { return curItemPage; } }
        public int ItemCountPerPage { get { return itemCountPerPage; } }
        public bool IsChangedPage { get { return prevItemPage != curItemPage; } }

        public MenuInfo(List<ItemInfo> itemsInfo, int itemCountPerPage)
        {
            this.itemsInfo = itemsInfo;
            this.itemCountPerPage = itemCountPerPage;
            PrevItemIndex = 0;
            CurItemIndex = 0;
        }
    }

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Text upText;
    [SerializeField] private Text downText;
    [SerializeField] private Transform contentTransform;
    [SerializeField] private GameObject itemPrefab;

    // UI Info
    private Rect viewportRect;
    private Rect itemRect;
    private float viewPortWidth;
    private float viewPortHeight;
    private float itemWidth;
    private float itemHeight;
    private int itemCountPerPage;

    // Menu Tree
    private TreeNode<MenuInfo> root; // Assume a tree is constructed
    private int curDepthIndex;
    private int curBreadthIndex;

    // Object Pool
    private int poolSize = 10;
    private List<GameObject> itemPool = new List<GameObject>();
    private List<GameObject> activeItems = new List<GameObject>();

    //public event System.Action<int> onSubmit;

    private void Awake()
    {
        Rect viewportRect = scrollRect.viewport.rect;
        Rect itemRect = itemPrefab.GetComponent<RectTransform>().rect;

        viewPortWidth = viewportRect.width;
        viewPortHeight = viewportRect.height;
        itemWidth = viewPortWidth;
        itemHeight = itemRect.height;
        itemCountPerPage = (int)(viewPortHeight / itemHeight);
    }

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newItem = Instantiate(itemPrefab, contentTransform);
            newItem.SetActive(false);
            itemPool.Add(newItem);
        }
    }

    public void SetRootMenu(List<string> itemsNames)
    {
        List<ItemInfo> itemsInfos = GetItemInfoList(itemsNames);

        root = new TreeNode<MenuInfo>(new MenuInfo(itemsInfos, itemCountPerPage)); // Assume a tree is constructed
    }

    private List<ItemInfo> GetItemInfoList(List<string> strings)
    {
        if (strings == null)
            return null;

        List<ItemInfo> itemsInfos = new List<ItemInfo>();

        foreach (string itemName in strings)
        {
            itemsInfos.Add(new ItemInfo(itemName));
        }

        return itemsInfos;
    }

    public void AddChildMenus(int parentDepthIndex, int childsPerParent, List<string> itemsNames)
    {
        List<ItemInfo> itemsInfos = GetItemInfoList(itemsNames);
        Tree<MenuInfo> tree = new Tree<MenuInfo>(root);
        int breadthCount = tree.GetBreadthCountAtDepth(parentDepthIndex);

        for (int i = 0;  i < breadthCount; i++)
        {
            TreeNode<MenuInfo> parent = root.FindNodeByIndices(parentDepthIndex, i);

            for (int j = 0; j < childsPerParent; j++)
            {
                TreeNode<MenuInfo> child = new TreeNode<MenuInfo>(new MenuInfo(itemsInfos, itemCountPerPage));
                parent.AddChild(child);
            }
        }
    }

    public void AddChildMenus(int parentDepthIndex, int parentBreadthIndex, int childCount, List<string> itemsNames)
    {
        List<ItemInfo> itemsInfos = GetItemInfoList(itemsNames);

        TreeNode<MenuInfo> parent = root.FindNodeByIndices(parentDepthIndex, parentBreadthIndex);

        for (int i = 0; i < childCount; i++)
        {
            TreeNode<MenuInfo> child = new TreeNode<MenuInfo>(new MenuInfo(itemsInfos, itemCountPerPage));
            parent.AddChild(child);
        }
    }

    public void RemoveAllMenusInTree()
    {
        root?.RemoveAllNodes();
    }

    private MenuInfo GetMenuInfoFromTree(int depthIndex, int breadthIndex)
    {
        TreeNode<MenuInfo> node = root.FindNodeByIndices(depthIndex, breadthIndex);

        if (node == null)
        {
            Debug.LogError($"depthIndex : {depthIndex}, breadthIndex : {breadthIndex}");

            return null;
        }

        return node.value;
    }

    public int ChangeMenu(int depthIndex, int breadthIndex, List<string> itemsNames = null)
    {
        curDepthIndex = depthIndex;
        curBreadthIndex = breadthIndex;

        DeactivateAllItem();

        MenuInfo menuInfo = GetMenuInfoFromTree(curDepthIndex, curBreadthIndex);

        if (itemsNames != null)
        {
            menuInfo.ItemsInfos = GetItemInfoList(itemsNames);
        }

        foreach (ItemInfo itemInfo in menuInfo.ItemsInfos)
        {
            ActivateItem(itemInfo);
        }

        SetUpAndDownText(menuInfo.ItemsInfos.Count, menuInfo.ItemCountPerPage);
        int index = SetInitialSelectedItem();

        return index;
    }

    public void ClearMenus()
    {
        DeactivateAllItem();
        SetUpAndDownText(false);
    }

    private int SetInitialSelectedItem()
    {
        MenuInfo menuInfo = GetMenuInfoFromTree(curDepthIndex, curBreadthIndex);
        int selectItemIndex = 0;

        for (int i = 0; i < activeItems.Count; i++)
        {
            selectItemIndex = (menuInfo.CurItemIndex + i) % activeItems.Count;
            ItemInfo itemInfo = activeItems[selectItemIndex].GetComponent<ScrollViewItemController>().ItemInfo;

            if (itemInfo.colorState == ItemInfo.ItemColorState.OriginColor)
            {
                break;
            }
        }

        SelectItem(selectItemIndex);

        return selectItemIndex;
    }

    private void ActivateItem(ItemInfo itemInfo)
    {
        // Activate an item from the pool and set its position and content
        GameObject newItem = GetNextInactiveItem();
        newItem.SetActive(true);
        newItem.GetComponent<ScrollViewItemController>().ItemInfo = itemInfo;
        activeItems.Add(newItem);
    }

    private void DeactivateItem(GameObject item)
    {
        // Deactivate an item and return it to the pool
        item.SetActive(false);
        activeItems.Remove(item);
        itemPool.Add(item);
    }

    public void DeactivateAllItem()
    {
        // Deactivate all item and return it to the pool
        foreach (GameObject item in activeItems)
        {
            item.SetActive(false);
            itemPool.Add(item);
        }

        activeItems.Clear();
    }

    private GameObject GetNextInactiveItem()
    {
        // Get the next inactive item from the pool
        GameObject item = null;

        foreach (GameObject obj in itemPool)
        {
            if (!obj.activeSelf)
            {
                item = obj;
                break;
            }
        }

        if (item == null)
        {
            // If there are no inactive items in the pool, instantiate a new one
            item = Instantiate(itemPrefab, contentTransform);
            itemPool.Add(item);
        }

        return item;
    }

    public void ResetAllMenu()
    {
        Tree<MenuInfo> tree = new Tree<MenuInfo>(root);

        // Perform in-order traversal
        List<MenuInfo> menusInfo = tree.InOrderTraversal();

        foreach (MenuInfo menuInfo in menusInfo)
        {
            menuInfo.PrevItemIndex = 0;
            menuInfo.CurItemIndex = 0;

            foreach (ItemInfo itemInfo in menuInfo.ItemsInfos)
            {
                itemInfo.colorState = ItemInfo.ItemColorState.OriginColor;
            }
        }
    }

    public void SetItemColorState(int depthIndex, int breadthIndex, int itemIndex, ItemInfo.ItemColorState colorState)
    {
        MenuInfo menuInfo = GetMenuInfoFromTree(depthIndex, breadthIndex);

        menuInfo.ItemsInfos[itemIndex].colorState = colorState;
    }


    public void SelectItem(int itemIndex)
    {
        MenuInfo menuInfo = GetMenuInfoFromTree(curDepthIndex, curBreadthIndex);

        menuInfo.PrevItemIndex = menuInfo.CurItemIndex = itemIndex;
        menuInfo.CurItemIndex = (menuInfo.CurItemIndex + activeItems.Count) % activeItems.Count;

        SetItemsColor(menuInfo.CurItemIndex);
        ChangePage(menuInfo.CurItemPage, menuInfo.ItemCountPerPage);
    }

    public int SelectItem(Vector2 vector)
    {
        MenuInfo menuInfo = GetMenuInfoFromTree(curDepthIndex, curBreadthIndex);

        menuInfo.PrevItemIndex = menuInfo.CurItemIndex;

        if (vector.y > 0)
            menuInfo.CurItemIndex = menuInfo.PrevItemIndex - 1;
        else if (vector.y < 0)
            menuInfo.CurItemIndex = menuInfo.PrevItemIndex + 1;

        menuInfo.CurItemIndex = (menuInfo.CurItemIndex + activeItems.Count) % activeItems.Count;

        SetItemsColor(menuInfo.CurItemIndex);

        if (menuInfo.IsChangedPage)
            ChangePage(menuInfo.CurItemPage, menuInfo.ItemCountPerPage);

        return menuInfo.CurItemIndex;
    }

    private void SetItemsColor(int selectedItemIndex)
    {
        for (int i = 0; i < activeItems.Count; i++)
        {
            ScrollViewItemController itemController = activeItems[i].GetComponent<ScrollViewItemController>();

            if (i == selectedItemIndex)
            {
                itemController.SetSelectedItemColor();
            }
            else
            {
                itemController.SetUnselectedItemColor();
            }
        }
    }

    private void SetUpAndDownText(int itemCount, int itemCountPerPage)
    {
        bool isPageOverflow = itemCount > itemCountPerPage;

        SetUpAndDownText(isPageOverflow);
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
        Vector2 targetItemLocalPosition = activeItems[targetItemIndex].GetComponent<RectTransform>().localPosition;

        Vector2 newTargetLocalPosition = new Vector2(
            0 - (viewPortLocalPosition.x + targetItemLocalPosition.x) + (viewPortWidth / 2) - (itemWidth / 2),
            0 - (viewPortLocalPosition.y + targetItemLocalPosition.y) + (viewPortHeight / 2) - (itemHeight / 2)
            );

        scrollRect.content.localPosition = newTargetLocalPosition;
    }

    public int SubmitItem()
    {
        MenuInfo menuInfo = GetMenuInfoFromTree(curDepthIndex, curBreadthIndex);

        int curItemIndex = menuInfo.CurItemIndex;

        return curItemIndex;
    }
}
