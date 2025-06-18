using UnityEngine;

public class ShieldUpgrade : Upgrade
{
    [SerializeField] private GameObject _shieldCanvas;

    protected override void AddUpgrade()
    {
        isUpgradePurchased = true;

        if (isUpgradePurchased)
        {
            _shieldCanvas.SetActive(true);
        }
    }
}
