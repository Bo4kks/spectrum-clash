using UnityEngine;

public class FunctionalUpgrade : Upgrade, IUpgrade
{
    [SerializeField] protected GameObject _featureObject;

    public void ActivateUpgrade() => AddUpgrade();

    protected override void AddUpgrade()
    {
        isUpgradePurchased = true;
    }
}
