using UnityEngine;
using DG.Tweening;

public class GameOverPanel : MonoBehaviour, IEventListener, IEventPusher
{
    [SerializeField] private CanvasGroup _gameOverScreen;
    [SerializeField] private float _fadeDurationInSeconds;

    private CanvasGroupSettings _canvasGroupSettings = new();

    public void OnEnable()
    {
        EventBus.Subscribe<OnGameOverEvent>(ShowGameOverScreen);
        EventBus.Subscribe<OnGameRestartEvent>(HideGameOverScreen);
        EventBus.Subscribe<OnPlayerGoToUpgradesShop>(InstantHideGameOverScreen);
        EventBus.Subscribe<OnPlayerQuitUpgradeShop>(InstantShowGameOverScreen);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnGameOverEvent>(ShowGameOverScreen);
        EventBus.Unsubscribe<OnGameRestartEvent>(HideGameOverScreen);
        EventBus.Unsubscribe<OnPlayerGoToUpgradesShop>(InstantHideGameOverScreen);
        EventBus.Unsubscribe<OnPlayerQuitUpgradeShop>(InstantShowGameOverScreen);
    }

    private void ShowGameOverScreen(OnGameOverEvent @event)
    {
        _canvasGroupSettings.SetCanvasGroupSettings(_gameOverScreen, true, false);

        _gameOverScreen.DOFade(1, _fadeDurationInSeconds);
        

        DOVirtual.DelayedCall(_fadeDurationInSeconds, () => EventBus.Invoke(new OnGameOverPanelFadeComplete()));
    }

    private void HideGameOverScreen(OnGameRestartEvent @event)
    {
        _gameOverScreen.DOFade(0f, _fadeDurationInSeconds);
        _canvasGroupSettings.SetCanvasGroupSettings(_gameOverScreen, false, false);
    }

    private void InstantHideGameOverScreen(OnPlayerGoToUpgradesShop @event)
    {
        _canvasGroupSettings.SetCanvasGroupSettings(_gameOverScreen, false);
    }

    private void InstantShowGameOverScreen(OnPlayerQuitUpgradeShop @event)
    {
        _canvasGroupSettings.SetCanvasGroupSettings(_gameOverScreen, true);
    }
}
