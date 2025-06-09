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
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnColorSwapEvent>(OnColorChanged);
        EventBus.Unsubscribe<OnGameOverEvent>(GameOver);
    }

    private void OnColorChanged(OnColorSwapEvent ev)
    {
        if (!_isGameOver)
        {
            _spriteRenderer.color = ev.NewColor;
        }
    }

    private void GameOver(OnGameOverEvent ev) => _isGameOver = true;
}
