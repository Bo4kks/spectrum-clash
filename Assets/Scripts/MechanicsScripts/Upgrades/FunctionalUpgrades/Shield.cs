using UnityEngine;

public class Shield : FunctionalUpgrade
{
    [SerializeField] private float _energyCostPerSecond;
    [SerializeField] private float _activationCost;

    private PlayerEnergy _playerEnergy;

    private bool _isShieldHeld = false;
    private bool _hasPaidActivationCost = false;

    private void Awake()
    {
        _playerEnergy = FindFirstObjectByType<PlayerEnergy>();
    }

    private void Update()
    {
        if (_isShieldHeld && CanActivateShield())
        {
            ActivateShield();
        }
        else
        {
            DeactivateShield();
        }
    }

    private bool CanActivateShield()
    {
        return _playerEnergy != null && _playerEnergy.CurrentEnergy >= _activationCost;
    }

    private void ActivateShield()
    {
        if (!_hasPaidActivationCost)
        {
            _playerEnergy.ConsumeEnergyInstantly(_activationCost);
            _hasPaidActivationCost = true;
        }

        _playerEnergy.ConsumeEnergyPerSecond(_energyCostPerSecond);
        _playerEnergy.SetIsEnergyRegenActive(false);
        _featureObject.SetActive(true);
        Debug.Log("Shield is active and consuming energy per second.");
    }

    private void DeactivateShield()
    {
        if (_hasPaidActivationCost)
        {
            _hasPaidActivationCost = false;
        }

        _playerEnergy.SetIsEnergyRegenActive(true);
        _featureObject.SetActive(false);
    }

    public void StartHoldingShield()
    {
        _isShieldHeld = true;
    }
    public void StopHoldingShield()
    {
        _isShieldHeld = false;
    }
}
