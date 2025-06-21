using UnityEngine;

public class ShieldSpellUI : MonoBehaviour, IEventListener
{
    private CanvasGroup _shieldSpellCanvasGroup;
    private CanvasGroupSettings _canvasGroupSettings = new();
    private Shield _shield;

    private void Awake()
    {
        _shieldSpellCanvasGroup = GetComponent<CanvasGroup>();
        _shield = FindFirstObjectByType<Shield>();
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnGameRestartEvent>(Show);
        EventBus.Subscribe<OnGameOverEvent>(Hide);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnGameRestartEvent>(Show);
        EventBus.Unsubscribe<OnGameOverEvent>(Hide);
    }

    private void Show(OnGameRestartEvent @event)
    {
        if (_shield.IsUpgradePurchased)
        {
            _canvasGroupSettings.SetCanvasGroupSettings(_shieldSpellCanvasGroup, true);
        }

        Debug.Log(_shield.IsUpgradePurchased);
    }

    private void Hide(OnGameOverEvent @event)
    {
        if (_shield.IsUpgradePurchased)
        {
            _canvasGroupSettings.SetCanvasGroupSettings(_shieldSpellCanvasGroup, false);
        }
    }

}
