using System;
using System.Collections.Generic;
using UnityEngine;

public class ColorSequence : MonoBehaviour, IEventListener, IEventPusher
{
    [SerializeField] private List<Color32> _colorsSequence;
    [SerializeField] private int _currentColorIndex = 0;

    public void OnEnable()
    {
        EventBus.Subscribe<OnTouchZonePressedEvent>(OnTouchZonePressed);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnTouchZonePressedEvent>(OnTouchZonePressed);
    }

    private void OnTouchZonePressed(OnTouchZonePressedEvent @event)
    {
        _currentColorIndex++;

        if (_currentColorIndex >= _colorsSequence.Count)
        {
            _currentColorIndex = 0;
        }

        Color32 color = _colorsSequence[_currentColorIndex];

        EventBus.Invoke(new OnColorSwapEvent(color));
    }

}
