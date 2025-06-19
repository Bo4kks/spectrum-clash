using UnityEngine;

public class CanvasGroupSettings
{
    public void SetCanvasGroupSettings(CanvasGroup canvasGroup, bool value, bool changeAlpha = true)
    {
        canvasGroup.blocksRaycasts = value;
        canvasGroup.interactable = value;

        if (!changeAlpha) return;

        if (value)
        {
            canvasGroup.alpha = 1f;
        }
        else
        {
            canvasGroup.alpha = 0f;
        }
    }
}
