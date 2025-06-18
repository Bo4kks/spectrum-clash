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
        EventBus.Subscribe<OnPlayerEarnedRedCurrencyEvent>(ChangeRedCurrencyValue);
        EventBus.Subscribe<OnPlayerEarnedYellowCurrencyEvent>(ChangeYellowCurrencyValue);
        EventBus.Subscribe<OnPlayerEarnedGreenCurrencyEvent>(ChangeGreenCurrencyValue);
        EventBus.Subscribe<OnPlayerEarnedBlueCurrencyEvent>(ChangeBlueCurrencyValue);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnPlayerEarnedRedCurrencyEvent>(ChangeRedCurrencyValue);
        EventBus.Unsubscribe<OnPlayerEarnedYellowCurrencyEvent>(ChangeYellowCurrencyValue);
        EventBus.Unsubscribe<OnPlayerEarnedGreenCurrencyEvent>(ChangeGreenCurrencyValue);
        EventBus.Unsubscribe<OnPlayerEarnedBlueCurrencyEvent>(ChangeBlueCurrencyValue);
    }

    private void ChangeRedCurrencyValue(OnPlayerEarnedRedCurrencyEvent @event) => _redCurrencyValue.text = @event.CurrencyValue.ToString();
    private void ChangeYellowCurrencyValue(OnPlayerEarnedYellowCurrencyEvent @event) => _yellowCurrencyValue.text = @event.CurrencyValue.ToString();
    private void ChangeGreenCurrencyValue(OnPlayerEarnedGreenCurrencyEvent @event) => _greenCurrencyValue.text = @event.CurrencyValue.ToString();
    private void ChangeBlueCurrencyValue(OnPlayerEarnedBlueCurrencyEvent @event) => _blueCurrencyValue.text = @event.CurrencyValue.ToString();

}
