using System;
using System.Collections.Generic;
using UnityEngine;

public class ColorSequence : MonoBehaviour
{
    [SerializeField] private List<Color32> _colorsSequence;
    [SerializeField] private int _currentColorIndex = 0;

    private void Awake()
    {
        EventBus.Subscribe<TouchZonePressedEvent>(OnTouchZonePressed);
    }
    private void OnDestroy()
    {
        EventBus.Unsubscribe<TouchZonePressedEvent>(OnTouchZonePressed);
    }

    private void OnTouchZonePressed(TouchZonePressedEvent @event)
    {
        _currentColorIndex++;

        if (_currentColorIndex >= _colorsSequence.Count)
        {
            _currentColorIndex = 0;
        }

        Color32 color = _colorsSequence[_currentColorIndex];

        EventBus.Invoke(new ColorSwapEvent(color));
    }

}
