public struct OnEnemyHitPlayerEvent
{
    public float Damage { get; private set; }

    public OnEnemyHitPlayerEvent(float damage)
    {
        Damage = damage;
    }
}
