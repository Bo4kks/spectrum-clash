using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    protected bool isUpgradePurchased = false;

    protected virtual void Initialize()
    {
        // Инизицализации апргейдов из json в будущем 
    }

    protected abstract void AddUpgrade();
}
