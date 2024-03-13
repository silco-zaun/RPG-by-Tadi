

using UnityEngine;

public class UIManager
{
    private static UIManager instance = null;

    private Color originColor;
    private Color selectedColor;
    private Color deactivateColor;

    public static UIManager Instance
    {
        get
        {
            if (instance == null)
                instance = new UIManager();

            return instance;
        }
    }

    public Color OriginColor { get { return originColor; } }
    public Color SelectedColor { get { return selectedColor; } }
    public Color DeactivateColor { get { return deactivateColor; } }

    private UIManager()
    {
        originColor = Color.white;
        selectedColor = HexToColor("#FFA500");
        deactivateColor = Color.gray;
    }

    public Color HexToColor(string hex)
    {
        Color color = Color.white; // Default color is white

        if (ColorUtility.TryParseHtmlString(hex, out color))
        {
            return color;
        }
        else
        {
            Debug.LogError("Invalid hexadecimal color code: " + hex);
            return Color.white;
        }
    }
}
