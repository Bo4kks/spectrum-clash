using UnityEngine;

public class StatsUpgrade : Upgrade, IUpgrade
{
    protected float maxHealth;
    protected float healthRegen;
    protected float maxEnergy;
    protected float energyRegen;
    protected int armor;

    public void ActivateUpgrade() => AddUpgrade();

    protected override void AddUpgrade()
    {
        isUpgradePurchased = true;
    }
}
