using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollViewItemController : MonoBehaviour
{
    private ItemInfo itemInfo;
    private Text text;

    public ItemInfo ItemInfo
    {
        get { return itemInfo; }
        set
        {
            itemInfo = value;
            text.text = itemInfo.name;
        }
    }

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUnselectedItemColor()
    {
        switch (itemInfo.colorState)
        {
            case ItemInfo.ItemColorState.OriginColor:
                text.color = UIManager.Instance.OriginColor;
                break;
            case ItemInfo.ItemColorState.DeactivatedColor:
                text.color = UIManager.Instance.DeactivateColor;
                break;
            default:
                break;
        }
    }

    public void SetSelectedItemColor()
    {
        GetComponent<Text>().color = UIManager.Instance.SelectedColor;
    }
}

public class ItemInfo
{
    public enum ItemColorState
    {
        OriginColor,
        DeactivatedColor
    }

    public string name;
    public Color textColor = UIManager.Instance.OriginColor;
    public ItemColorState colorState;

    public ItemInfo() { }

    public ItemInfo(string name)
    {
        this.name = name;
    }
}
