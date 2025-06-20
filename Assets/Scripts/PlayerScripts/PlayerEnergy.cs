using UnityEngine;

public class PlayerEnergy : MonoBehaviour
{
    [SerializeField] private float _currentEnergy;
    [SerializeField] private bool _isEnergyRegenActive = false;
    [SerializeField] private bool _canRegenerate = true;

    private PlayerStats _playerStats;

    public float CurrentEnergy
    {
        get
        {
            return _currentEnergy;
        }
        private set
        {
            _currentEnergy = value;
            EventBus.Invoke(new OnEnergyValueChanged(_currentEnergy));
        }
    }

    private void Awake()
    {
        _playerStats = GetComponent<PlayerStats>();
    }

    public void Start()
    {
        SetEnergyToMaxEnergyValue();
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnGameRestartEvent>(SetEnergyToMaxEnergyValue);
        EventBus.Subscribe<OnGameOverEvent>(StopRegenerate);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnGameRestartEvent>(SetEnergyToMaxEnergyValue);
        EventBus.Unsubscribe<OnGameOverEvent>(StopRegenerate);
    }

    private void Update()
    {
        if (CurrentEnergy < _playerStats.GetFloat(FloatStatType.MaxEnergy) && _isEnergyRegenActive && _canRegenerate)
        {
            CurrentEnergy += _playerStats.GetFloat(FloatStatType.EnergyRegenPerSecond) * Time.deltaTime;
            CurrentEnergy = Mathf.Min(CurrentEnergy, _playerStats.GetFloat(FloatStatType.MaxEnergy));
        }
    }

    private void SetEnergyToMaxEnergyValue(OnGameRestartEvent @event)
    {
        CurrentEnergy = _playerStats.GetFloat(FloatStatType.MaxEnergy);
        _canRegenerate = true;
    }

    private void StopRegenerate(OnGameOverEvent @event) => _canRegenerate = false;

    private void SetEnergyToMaxEnergyValue() => _currentEnergy = _playerStats.GetFloat(FloatStatType.MaxEnergy);

    public void ConsumeEnergyPerSecond(float valuePerSecond)
    {
        CurrentEnergy -= valuePerSecond * Time.deltaTime;
        CurrentEnergy = Mathf.Max(CurrentEnergy, 0f);
    }

    public void ConsumeEnergyInstantly(float value)
    {
        CurrentEnergy -= value;
        CurrentEnergy = Mathf.Max(CurrentEnergy, 0f);
    }

    public void SetIsEnergyRegenActive(bool value) => _isEnergyRegenActive = value;
}
