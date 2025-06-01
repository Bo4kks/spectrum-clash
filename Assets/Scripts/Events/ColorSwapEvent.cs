using UnityEngine;

public struct ColorSwapEvent
{
    public Color32 newColor;
    public ColorSwapEvent(Color32 color)
    {
        newColor = color;
    }
}
