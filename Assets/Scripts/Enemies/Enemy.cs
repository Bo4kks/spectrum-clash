using UnityEngine;

public abstract class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] private EnemyDataSO _enemyData;
    [SerializeField] protected float speed;
    [SerializeField] protected Color32 color;
    [SerializeField] protected float damage;
    protected Transform target;
    protected SpriteRenderer spriteRenderer;
    protected EnemyPool pool;

    public Color32 Color { get => color; }
    public EnemyDataSO EnemyData { get => _enemyData; }

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public virtual void Initialize(EnemyDataSO data)
    {
        target = FindFirstObjectByType<PlayerHealth>().transform;
        speed = data.Speed;
        color = data.Color;
        damage = data.Damage;

        if (spriteRenderer != null)
        {
            spriteRenderer.color = color;
        }
    }

    public void SetPool(EnemyPool enemyPool)
    {
        pool = enemyPool;
    }

    protected virtual void FixedUpdate()
    {
        Move();
    }

    protected abstract void Move();

    public virtual void ReturnToPool()
    {
        pool.ReturnToPool(gameObject, _enemyData);
    }

    public virtual void OnHitPlayer()
    {
        EventBus.Invoke(new OnEnemyHitPlayerEvent(damage));
        ReturnToPool();
    }
}
