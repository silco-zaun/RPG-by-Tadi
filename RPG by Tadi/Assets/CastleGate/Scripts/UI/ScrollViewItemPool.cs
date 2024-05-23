using System.Collections.Generic;
using Tadi.UI.ScrollView;
using UnityEngine;

public class ScrollViewItemPool : MonoBehaviour
{
    [SerializeField] private Transform contentTransform;
    [SerializeField] private GameObject itemPrefab;

    // Object Pool
    private int poolSize = 10;
    private List<GameObject> itemPool = new List<GameObject>();
    private List<GameObject> activeItems = new List<GameObject>();

    public Rect ItemRect { get { return itemPrefab.GetComponent<RectTransform>().rect; } }
    public int ItemsCount { get { return activeItems.Count; } }

    private void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newItem = Instantiate(itemPrefab, contentTransform);
            newItem.SetActive(false);
            itemPool.Add(newItem);
        }
    }

    public int GetInitIndex(int curItemIndex)
    {
        int selectItemIndex = 0;

        for (int i = 0; i < activeItems.Count; i++)
        {
            selectItemIndex = (curItemIndex + i) % activeItems.Count;

            if (GetItemColorState(selectItemIndex) == ItemState.Origin)
                break;
        }

        return selectItemIndex;
    }

    public ItemState GetItemColorState(int itemIndex)
    {
        ItemState colorState = activeItems[itemIndex].GetComponent<ScrollViewItemController>().ItemInfo.ColorState;

        return colorState;
    }

    private void DeactivateItem(GameObject item)
    {
        // Deactivate an item and return it to the pool
        item.SetActive(false);
        activeItems.Remove(item);
    }

    public void DeactivateAllItem()
    {
        // Deactivate all item and return it to the pool
        activeItems.ForEach(i => i.SetActive(false));
        activeItems.Clear();
    }

    public void ActivateItem(ItemInfo itemInfo)
    {
        // Activate an item from the pool and set its position and content
        GameObject newItem = GetNextInactiveItem();
        newItem.SetActive(true);
        newItem.GetComponent<ScrollViewItemController>().Init(itemInfo);
        activeItems.Add(newItem);
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

    public void SetItemsColor(int selectedItemIndex)
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

    public Vector2 GetItemLocalPostion(int itemIndex)
    {
        return activeItems[itemIndex].GetComponent<RectTransform>().localPosition;
    }
}
