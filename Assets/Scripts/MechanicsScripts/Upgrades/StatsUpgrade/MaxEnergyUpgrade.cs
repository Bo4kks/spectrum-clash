using System.Collections.Generic;
using UnityEngine;

public class MaxEnergyUpgrade : StatsUpgrade
{
    [SerializeField] private float _maxEnergyPerLvl;

    protected override void ApplyUpgrade()
    {
        var floatStats = new Dictionary<FloatStatType, float>
        {
            { FloatStatType.MaxEnergy, _maxEnergyPerLvl  }
        };

        var intStats = new Dictionary<IntStatType, int>();


        var boolStats = new Dictionary<BoolStatType, bool>();

        EventBus.Invoke(new OnStatsUpgradeEvent(floatStats, intStats, boolStats));
    }
}
