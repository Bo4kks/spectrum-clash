using UnityEngine;

public class ShieldSpellUI : MonoBehaviour, IEventListener
{
    private CanvasGroup _shieldSpellCanvasGroup;
    private CanvasGroupSettings _canvasGroupSettings = new();

    private void Awake()
    {
        _shieldSpellCanvasGroup = GetComponent<CanvasGroup>();
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
        _canvasGroupSettings.SetCanvasGroupSettings(_shieldSpellCanvasGroup, true);
    }

    private void Hide(OnGameOverEvent @event)
    {
        _canvasGroupSettings.SetCanvasGroupSettings(_shieldSpellCanvasGroup, false);
    }

}
