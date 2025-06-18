using UnityEngine;
using UnityEngine.UI;

public class EnergyBarUI : MonoBehaviour, IEventListener
{
    private Image _energyBar;

    private void Awake()
    {
        _energyBar = GetComponent<Image>();
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnEnergyValueChanged>(RefillEnergyBar);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnEnergyValueChanged>(RefillEnergyBar);
    }

    private void RefillEnergyBar(OnEnergyValueChanged @event)
    {
        float currentEnergy = @event.EnergyValue;

        _energyBar.fillAmount = currentEnergy / 100f;
    }
}
