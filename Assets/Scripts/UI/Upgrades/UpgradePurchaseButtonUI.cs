using UnityEngine;
using UnityEngine.UI;

public class UpgradePurchaseButtonUI : MonoBehaviour
{
    [Header("Стоимость и валюта")]
    [SerializeField] private CurrencyTypes _currencyType;
    [SerializeField] private int _price;

    [Header("Ссылка на апгрейд")]
    [Tooltip("Любой компонент, реализующий IUpgradeAction")]
    [SerializeField] private MonoBehaviour _upgradeComponent;

    private Button _button;
    private PlayerCurrency _currency;
    private IUpgrade _upgradeAction;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _currency = FindFirstObjectByType<PlayerCurrency>();

        _upgradeAction = _upgradeComponent as IUpgrade;

        if (_upgradeAction == null)
        {
            Debug.LogError($"[{name}] {_upgradeComponent.name} не реализует IUpgradeAction");
        }
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(TryPurchase);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(TryPurchase);
    }

    private void TryPurchase()
    {
        if (_currency == null || _upgradeAction == null)
        {
            Debug.LogError($"[{name}] Не удалось выполнить покупку: отсутствует валюта или компонент апгрейда");
        }

        if (_currency.TrySpend(_currencyType, _price))
        {
            _upgradeAction.ActivateUpgrade();

            _button.interactable = false;

            Debug.Log($"Улучшение {_upgradeComponent.name} куплено за {_price} {_currencyType}");
        }
        else
        {
            Debug.Log($"Недостаточно {_currencyType} для покупки {_upgradeComponent.name}");
        }
    }
}
