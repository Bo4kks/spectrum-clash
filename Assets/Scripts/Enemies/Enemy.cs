using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(EnemyDeathEffect))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public abstract class Enemy : MonoBehaviour, IEnemy, IEventPusher
{
    [SerializeField] private EnemyDataSO _enemyData;
    [SerializeField] private EnemiesDifficulty _enemiesDifficulty;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected Color32 color;

    protected Transform target;
    protected SpriteRenderer spriteRenderer;
    protected EnemyPool pool;
    protected bool _isDead = false;
    protected Vector3 _originalScale;

    private EnemyDeathEffect _deathEffect;
    private float _killDelay;

    public Color32 Color { get => color; }
    public EnemyDataSO EnemyData { get => _enemyData; }

    protected virtual void Awake()
    {
        _deathEffect = GetComponent<EnemyDeathEffect>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        _enemiesDifficulty = FindFirstObjectByType<EnemiesDifficulty>();

        _killDelay = _deathEffect.FadeDuration + 0.1f;
        _originalScale = transform.localScale;
    }

    public virtual void Initialize(EnemyDataSO data)
    {
        _isDead = false;

        target = FindFirstObjectByType<PlayerHealth>().transform;
        color = data.Color;
        speed = data.Speed * _enemiesDifficulty.SpeedMultiplier;
        damage = data.Damage * _enemiesDifficulty.DamageMultiplier;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }

    public void SetPool(EnemyPool enemyPool) => pool = enemyPool;

    protected virtual void FixedUpdate() => Move();

    protected abstract void Move();

    public virtual void Kill()
    {
        if (_isDead) return;
        _isDead = true;

        _deathEffect.PlayDeathEffect();

        enabled = false;

        DOVirtual.DelayedCall(_killDelay, () =>
        {

            ReturnToPool();

            _isDead = false;
            enabled = true;

            spriteRenderer.color = Color;
            transform.localScale = _originalScale;
        });
    }

    protected virtual void ReturnToPool()
    {
        pool.ReturnToPool(gameObject, _enemyData);
    }

    public virtual void OnHitPlayer()
    {
        EventBus.Invoke(new OnEnemyHitPlayerEvent(damage));
        Kill();
    }
}
