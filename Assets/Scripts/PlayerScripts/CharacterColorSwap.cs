using UnityEngine;

public class CharacterColorSwap : MonoBehaviour, IEventListener
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnColorSwapEvent>(OnColorChanged);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnColorSwapEvent>(OnColorChanged);
    }

    private void OnColorChanged(OnColorSwapEvent ev)
    {
        _spriteRenderer.color = ev.NewColor;
    }
}
