using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class BattleMenu : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private Text upText;
    [SerializeField] private Text downText;
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Color originColor;
    [SerializeField] private Color highlightColor;
    [SerializeField] private Color deactivateColor;

    private List<Text> itemTexts = new List<Text>();
    private float viewPortWidth;
    private float viewPortHeight;
    private float itemWidth;
    private float itemHeight;
    private int itemCount;
    private int itemCountPerPage;
    private int prevItemIndex = 0;
    private int curItemIndex = 0;
    private int prevItemPage;
    private int curItemPage;

    //public event System.Action<int> onSubmit;

    private void Start()
    {
    }

    public void SetMenu(List<string> itemNames)
    {
        foreach (Text itemText in itemTexts)
        {
            Destroy(itemText.gameObject);
        }

        itemTexts.Clear(); // 자동으로 Destroy 될라나?

        foreach (string name in itemNames)
        {
            GameObject instantiatedPrefab = Instantiate(itemPrefab, content.transform);
            instantiatedPrefab.GetComponent<Text>().text = name;
            itemTexts.Add(instantiatedPrefab.GetComponent<Text>());
        }

        HighlightSelectedItem(prevItemIndex, curItemIndex);

        viewPortWidth = scrollRect.viewport.rect.width;
        viewPortHeight = scrollRect.viewport.rect.height;
        itemWidth = itemTexts[0].rectTransform.rect.width;
        itemHeight = itemTexts[0].rectTransform.rect.height;
        itemCount = itemTexts.Count;
        itemCountPerPage = (int)(viewPortHeight / itemHeight);

        bool isPageOverflow = itemCount > itemCountPerPage;
        SetActivationUpAndDownText(isPageOverflow);
    }

    public void SelectMenu(Vector2 vector)
    {
        prevItemIndex = curItemIndex;
        prevItemPage = (prevItemIndex / itemCountPerPage) + 1;

        if (vector.y > 0)
            curItemIndex = prevItemIndex - 1;
        else if (vector.y < 0)
            curItemIndex = prevItemIndex + 1;

        curItemIndex = (curItemIndex + itemTexts.Count) % itemTexts.Count;
        curItemPage = (curItemIndex / itemCountPerPage) + 1;

        HighlightSelectedItem(prevItemIndex, curItemIndex);
        ChangePage(prevItemPage, curItemPage, itemCountPerPage);
    }

    private void HighlightSelectedItem(int prevItemIndex, int curItemIndex)
    {
        itemTexts[prevItemIndex].color = originColor;
        itemTexts[curItemIndex].color = highlightColor;
    }

    private void SetActivationUpAndDownText(bool activating)
    {
        if (activating)
        {
            upText.color = originColor;
            downText.color = originColor;
        }
        else
        {
            upText.color = deactivateColor;
            downText.color = deactivateColor;
        }
    }

    private void ChangePage(int prevItemPage, int curItemPage, int itemCountPerPage)
    {
        bool isChangedPage = prevItemPage != curItemPage;
        int firstItemIndexInPage = (curItemPage - 1) * itemCountPerPage;

        if (isChangedPage)
            ScrollToTargetItem(firstItemIndexInPage);
    }

    private void ScrollToTargetItem(int targetItemIndex)
    {
        //Canvas.ForceUpdateCanvases();
        Vector2 viewPortLocalPosition = scrollRect.viewport.localPosition;
        Vector2 targetItemLocalPosition = itemTexts[targetItemIndex].rectTransform.localPosition;

        Vector2 newTargetLocalPosition = new Vector2(
            0 - (viewPortLocalPosition.x + targetItemLocalPosition.x) + (viewPortWidth / 2) - (itemWidth / 2),
            0 - (viewPortLocalPosition.y + targetItemLocalPosition.y) + (viewPortHeight / 2) - (itemHeight / 2)
            );

        scrollRect.content.localPosition = newTargetLocalPosition;
    }

    public int SubmitItem()
    {
        //onSubmit(curItemIndex);

        return curItemIndex;
    }
}
