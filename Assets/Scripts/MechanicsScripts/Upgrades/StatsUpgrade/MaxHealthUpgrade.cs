using UnityEngine;
using System.Collections.Generic;

public class MaxHealthUpgrade : StatsUpgrade
{
    [SerializeField] private float _healthBonusPerLevel = 10f;

    protected override void ApplyUpgrade()
    {
        var floatStats = new Dictionary<FloatStatType, float>
        {
            { FloatStatType.MaxHealth, _healthBonusPerLevel }
        };
        
        var intStats = new Dictionary<IntStatType, int>();
        var boolStats = new Dictionary<BoolStatType, bool>();

        EventBus.Invoke(new OnStatsUpgradeEvent(floatStats, intStats, boolStats));
    }
}

