using TMPro;
using UnityEngine;

public class CurrencyValueUIPanel : MonoBehaviour, IEventListener
{
    [SerializeField] private TextMeshProUGUI _redCurrencyValue;
    [SerializeField] private TextMeshProUGUI _yellowCurrencyValue;
    [SerializeField] private TextMeshProUGUI _greenCurrencyValue;
    [SerializeField] private TextMeshProUGUI _blueCurrencyValue;

    public void OnEnable()
    {
        EventBus.Subscribe<OnRedCurrencyValueChanged>(UpdateRedCurrencyFields);
        EventBus.Subscribe<OnYellowCurrencyValueChanged>(UpdateYellowCurrencyFields);
        EventBus.Subscribe<OnGreenCurrencyValueChanged>(UpdateGreenValueFields);
        EventBus.Subscribe<OnBlueCurrencyValueChanged>(UpdateBlueValueFields);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnRedCurrencyValueChanged>(UpdateRedCurrencyFields);
        EventBus.Unsubscribe<OnYellowCurrencyValueChanged>(UpdateYellowCurrencyFields);
        EventBus.Unsubscribe<OnGreenCurrencyValueChanged>(UpdateGreenValueFields);
        EventBus.Unsubscribe<OnBlueCurrencyValueChanged>(UpdateBlueValueFields);
    }

    private void UpdateRedCurrencyFields(OnRedCurrencyValueChanged @event) => _redCurrencyValue.text = @event.RedCurrencyValue.ToString();
    private void UpdateYellowCurrencyFields(OnYellowCurrencyValueChanged @event) => _yellowCurrencyValue.text = @event.YellowCurrencyValue.ToString();
    private void UpdateGreenValueFields(OnGreenCurrencyValueChanged @event) => _greenCurrencyValue.text = @event.GreenCurrencyValue.ToString();
    private void UpdateBlueValueFields(OnBlueCurrencyValueChanged @event) => _blueCurrencyValue.text = @event.BlueCurrencyValue.ToString();

}
