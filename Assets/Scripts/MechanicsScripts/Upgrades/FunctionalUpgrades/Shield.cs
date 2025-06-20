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
        if (_isShieldHeld)
        {
            if (!_hasPaidActivationCost && _playerEnergy.CurrentEnergy >= _activationCost)
            {
                ActivateShield();
            }

            if (_hasPaidActivationCost && _playerEnergy.CurrentEnergy > 0f)
            {
                MaintainShield();
            }
            else if (_hasPaidActivationCost && _playerEnergy.CurrentEnergy <= 0f)
            {
                DeactivateShield();
            }
        }
        else
        {
            if (_hasPaidActivationCost)
            {
                DeactivateShield();
            }
        }
    }

    private void ActivateShield()
    {
        _playerEnergy.ConsumeEnergyInstantly(_activationCost);
        _hasPaidActivationCost = true;

        _featureObject.SetActive(true);
        _playerEnergy.SetIsEnergyRegenActive(false);
    }

    private void MaintainShield()
    {
        _playerEnergy.ConsumeEnergyPerSecond(_energyCostPerSecond);
    }

    private void DeactivateShield()
    {
        _hasPaidActivationCost = false;
        _featureObject.SetActive(false);
        _playerEnergy.SetIsEnergyRegenActive(true);
    }

    public void StartHoldingShield() => _isShieldHeld = true;
    public void StopHoldingShield() => _isShieldHeld = false;
}
