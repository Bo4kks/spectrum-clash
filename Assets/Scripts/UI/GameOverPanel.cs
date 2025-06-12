using System;
using UnityEngine;
using DG.Tweening;

public class GameOverPanel : MonoBehaviour, IEventListener, IEventPusher
{
    [SerializeField] private CanvasGroup _gameOverScreen;
    [SerializeField] private float _fadeDurationInSeconds;

    public void OnEnable()
    {
        EventBus.Subscribe<OnGameOverEvent>(ShowGameOverScreen);
        EventBus.Subscribe<OnGameRestartEvent>(HideGameOverScreen);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnGameOverEvent>(ShowGameOverScreen);
        EventBus.Unsubscribe<OnGameRestartEvent>(HideGameOverScreen);
    }

    private void ShowGameOverScreen(OnGameOverEvent @event)
    {
        _gameOverScreen.DOFade(1, _fadeDurationInSeconds);

        DOVirtual.DelayedCall(_fadeDurationInSeconds, () => EventBus.Invoke(new OnGameOverPanelFadeComplete()));
    }

    private void HideGameOverScreen(OnGameRestartEvent @event)
    {
        _gameOverScreen.DOFade(0f, _fadeDurationInSeconds);
    }


}
