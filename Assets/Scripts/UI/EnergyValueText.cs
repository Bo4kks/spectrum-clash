using TMPro;
using UnityEngine;

public class EnergyValueText : MonoBehaviour
{
    private TextMeshProUGUI _energyValueText;

    private void Awake()
    {
        _energyValueText = GetComponent<TextMeshProUGUI>();
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnEnergyValueChanged>(ChangeEnergyValue);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnEnergyValueChanged>(ChangeEnergyValue);
    }

    private void ChangeEnergyValue(OnEnergyValueChanged @event)
    {
        _energyValueText.text = @event.EnergyValue.ToString("#.##");
    }
}
