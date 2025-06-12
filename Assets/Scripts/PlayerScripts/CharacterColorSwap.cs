using UnityEngine;

public class CharacterColorSwap : MonoBehaviour, IEventListener
{
    private SpriteRenderer _spriteRenderer;
    private bool _isGameOver = false;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnColorSwapEvent>(OnColorChanged);
        EventBus.Subscribe<OnGameOverEvent>(GameOver);
        EventBus.Subscribe<OnGameRestartEvent>(GameRestart);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnColorSwapEvent>(OnColorChanged);
        EventBus.Unsubscribe<OnGameOverEvent>(GameOver);
        EventBus.Unsubscribe<OnGameRestartEvent>(GameRestart);
    }

    private void OnColorChanged(OnColorSwapEvent ev)
    {
        if (!_isGameOver)
        {
            _spriteRenderer.color = ev.NewColor;
        }
    }

    private void GameOver(OnGameOverEvent @event) => _isGameOver = true;

    private void GameRestart(OnGameRestartEvent @event) => _isGameOver = false;
}
