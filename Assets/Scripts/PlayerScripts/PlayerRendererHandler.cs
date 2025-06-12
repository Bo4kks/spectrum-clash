using UnityEngine;

public class PlayerRendererHandler : MonoBehaviour, IEventListener
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnGameOverEvent>(TurnOffSpriteRenderer);
        EventBus.Subscribe<OnGameRestartEvent>(TurnOnSpriteRenderer);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnGameOverEvent>(TurnOffSpriteRenderer);
        EventBus.Unsubscribe<OnGameRestartEvent>(TurnOnSpriteRenderer);
    }

    private void TurnOffSpriteRenderer(OnGameOverEvent ev) => _spriteRenderer.enabled = false;

    private void TurnOnSpriteRenderer(OnGameRestartEvent ev) => _spriteRenderer.enabled = true;
}
