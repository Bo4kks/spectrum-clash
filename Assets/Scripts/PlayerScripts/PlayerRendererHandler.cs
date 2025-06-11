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
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnGameOverEvent>(TurnOffSpriteRenderer);
    }

    private void TurnOffSpriteRenderer(OnGameOverEvent ev) => _spriteRenderer.enabled = false;

    private void TurnOnSpriteRenderer() => _spriteRenderer.enabled = true;
}
