using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tadi.Datas.UI
{
    public static class UIData
    {
        public static Color OriginColor { get; private set; }
        public static Color SelectedColor { get; private set; }
        public static Color DeactivateColor { get; private set; }

        static UIData()
        {
            OriginColor = Color.white;
            SelectedColor = HexToColor("#FFA500");
            DeactivateColor = Color.gray;
        }

        public static  Color HexToColor(string hex)
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
}
