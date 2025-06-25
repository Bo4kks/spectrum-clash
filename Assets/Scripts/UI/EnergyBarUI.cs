using UnityEngine;
using UnityEngine.UI;

public class EnergyBarUI : MonoBehaviour, IEventListener
{
    [SerializeField] private Image _energyBar;

    private CanvasGroup _energyBarCanvas;
    private CanvasGroupSettings _canvasGroupSettings = new();
    private PlayerStats _playerStats;

    private void Awake()
    {
        _energyBarCanvas = GetComponent<CanvasGroup>();
        _playerStats = FindFirstObjectByType<PlayerStats>();
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnEnergyValueChanged>(RefillEnergyBar);
        EventBus.Subscribe<OnPlayerGoToUpgradesShop>(HideEnergyBar);
        EventBus.Subscribe<OnPlayerQuitUpgradeShop>(ShowEnergyBar);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnEnergyValueChanged>(RefillEnergyBar);
        EventBus.Unsubscribe<OnPlayerGoToUpgradesShop>(HideEnergyBar);
        EventBus.Unsubscribe<OnPlayerQuitUpgradeShop>(ShowEnergyBar);
    }

    private void RefillEnergyBar(OnEnergyValueChanged @event)
    {
        float currentEnergy = @event.EnergyValue;

        _energyBar.fillAmount = currentEnergy / _playerStats.GetFloat(FloatStatType.MaxEnergy);
    }

    private void HideEnergyBar(OnPlayerGoToUpgradesShop @event)
    {
        _canvasGroupSettings.SetCanvasGroupSettings(_energyBarCanvas, false);
    }

    private void ShowEnergyBar(OnPlayerQuitUpgradeShop @event)
    {
        _canvasGroupSettings.SetCanvasGroupSettings(_energyBarCanvas, true);
    }
}
