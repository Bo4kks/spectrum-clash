public struct OnEnemiesDifficultyMultiplierEvent
{
    public float DamageMultiplier { get; private set; }
    public float SpeedMultiplier { get; private set; }

    public OnEnemiesDifficultyMultiplierEvent(float damageMultiplier, float speedMultiplier)
    {
        DamageMultiplier = damageMultiplier;
        SpeedMultiplier = speedMultiplier;
    }
}
