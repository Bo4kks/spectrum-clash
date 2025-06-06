using UnityEngine;

public struct OnColorSwapEvent
{
    public Color32 NewColor { get; private set; }

    public OnColorSwapEvent(Color32 color)
    {
        NewColor = color;
    }
}
