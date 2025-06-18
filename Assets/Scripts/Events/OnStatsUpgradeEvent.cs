using System.Collections.Generic;

public class OnStatsUpgradeEvent
{
    public Dictionary<FloatStatType, float> FloatStats { get; }
    public Dictionary<IntStatType, int> IntStats { get; }
    public Dictionary<BoolStatType, bool> BoolStats { get; }

    public OnStatsUpgradeEvent(
        Dictionary<FloatStatType, float> floatStats = null,
        Dictionary<IntStatType, int> intStats = null,
        Dictionary<BoolStatType, bool> boolStats = null)
    {
        FloatStats = floatStats ?? new();
        IntStats = intStats ?? new();
        BoolStats = boolStats ?? new();
    }
}