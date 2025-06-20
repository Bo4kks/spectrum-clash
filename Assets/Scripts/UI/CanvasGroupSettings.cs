using UnityEngine;
using UnityEngine.UI;

public class CanvasGroupSettings
{
    public void SetCanvasGroupSettings(CanvasGroup canvasGroup, bool value, bool changeAlpha = true, bool changeInteractable = true)
    {
        canvasGroup.blocksRaycasts = value;

        if (changeAlpha)
        {
            canvasGroup.alpha = value ? 1f : 0f;
        }

        if (changeInteractable)
        {
            canvasGroup.interactable = value;
        }
    }

    public void SetCanvasGroupSettingsWithException(CanvasGroup canvasGroup, bool value, Image exception, bool changeAlpha = true, bool changeInteractable = true)
    {
        canvasGroup.blocksRaycasts = value;

        if (changeAlpha)
        {
            canvasGroup.alpha = value ? 1f : 0f;
        }

        if (changeInteractable)
        {
            canvasGroup.interactable = value;
        }
 
        Color exceptionColor = exception.color;

        exceptionColor.a = value ? 1f : 0f;

        exception.color = exceptionColor;

        exception.raycastTarget = value;
    }
}
