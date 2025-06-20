public interface IStatsUpgrade
{
    int CurrentLevel { get; }
    int MaxLevel { get; }
    bool TryPurchase();
}
