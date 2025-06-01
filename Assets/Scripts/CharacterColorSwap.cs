using System;
using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class CharacterColorSwap : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        EventBus.Subscribe<ColorSwapEvent>(OnColorChanged);
    }

    private void OnColorChanged(ColorSwapEvent ev)
    {
        _spriteRenderer.color = ev.newColor;
    }
}
