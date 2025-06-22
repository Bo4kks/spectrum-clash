using System.Collections.Generic;
using UnityEngine;

public class DefenseUpgrade : StatsUpgrade
{
    [SerializeField] private int _defenseBonusPerLevel;

    protected override void ApplyUpgrade()
    {
        var floatStats = new Dictionary<FloatStatType, float>();

        var intStats = new Dictionary<IntStatType, int>
        {
            { IntStatType.Armor, _defenseBonusPerLevel }
        };

        var boolStats = new Dictionary<BoolStatType, bool>();

        EventBus.Invoke(new OnStatsUpgradeEvent(floatStats, intStats, boolStats));
    }
}
