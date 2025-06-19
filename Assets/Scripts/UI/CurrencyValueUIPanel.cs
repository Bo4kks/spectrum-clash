using TMPro;
using UnityEngine;

public class CurrencyValueUIPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _redCurrencyValue;
    [SerializeField] private TextMeshProUGUI _yellowCurrencyValue;
    [SerializeField] private TextMeshProUGUI _greenCurrencyValue;
    [SerializeField] private TextMeshProUGUI _blueCurrencyValue;

    private PlayerCurrency _playerCurrency;

    private void Awake()
    {
        _playerCurrency = FindFirstObjectByType<PlayerCurrency>();
    }

    private void OnEnable()
    {
        UpdateCurrencyValue();
    }

    private void UpdateCurrencyValue()
    {
        _redCurrencyValue.text = _playerCurrency.RedCurrency.ToString();
        _yellowCurrencyValue.text = _playerCurrency.YellowCurrency.ToString();
        _greenCurrencyValue.text = _playerCurrency.GreenCurrency.ToString();
        _blueCurrencyValue.text = _playerCurrency.BlueCurrency.ToString();
    }


}
