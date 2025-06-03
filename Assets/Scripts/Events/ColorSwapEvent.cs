using UnityEngine;

public struct ColorSwapEvent
{
    public Color32 NewColor { get; private set; }

    public ColorSwapEvent(Color32 color)
    {
        NewColor = color;
    }
}
