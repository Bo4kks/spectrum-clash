using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    [SerializeField] protected bool isUpgradePurchased = false;

    public bool IsUpgradePurchased { get { return isUpgradePurchased; } }

    protected virtual void Initialize()
    {
        // �������������� ��������� �� json � ������� 
    }

    protected abstract void AddUpgrade();
}
