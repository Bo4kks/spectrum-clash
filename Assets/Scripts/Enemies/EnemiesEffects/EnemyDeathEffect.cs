using UnityEngine;
using DG.Tweening;

public class EnemyDeathEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _deathParticles;
    [SerializeField] private float _fadeDuration = 0.5f;
    [SerializeField] private Ease _fadeEase = Ease.InQuad;

    private SpriteRenderer _spriteRenderer;

    public float FadeDuration { get { return _fadeDuration; } }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void PlayDeathEffect()
    {
        if (_deathParticles != null)
        {
            var ps = Instantiate(_deathParticles, transform.position, Quaternion.identity);

            var main = ps.main;

            main.startColor = _spriteRenderer.color;

            Destroy(ps.gameObject, main.duration + main.startLifetime.constantMax);
        }

        _spriteRenderer.DOFade(0f, _fadeDuration).SetEase(_fadeEase);
        transform.DOScale(Vector3.zero, _fadeDuration).SetEase(_fadeEase);
    }
}
