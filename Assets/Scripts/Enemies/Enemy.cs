using System;
using UnityEngine;

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

    public Color32 Color { get => color; }
    public EnemyDataSO EnemyData { get => _enemyData; }

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        _enemiesDifficulty = FindFirstObjectByType<EnemiesDifficulty>();
    }

    public virtual void Initialize(EnemyDataSO data)
    {
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

    public virtual void ReturnToPool() => pool.ReturnToPool(gameObject, _enemyData);

    public virtual void OnHitPlayer()
    {
        EventBus.Invoke(new OnEnemyHitPlayerEvent(damage));
        ReturnToPool();
    }
}
