using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    protected bool isUpgradePurchased = false;

    protected virtual void Initialize()
    {
        // �������������� ��������� �� json � ������� 
    }

    protected abstract void AddUpgrade();
}
