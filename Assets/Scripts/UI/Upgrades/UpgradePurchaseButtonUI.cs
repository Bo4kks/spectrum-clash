using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePurchaseButtonUI : MonoBehaviour, IEventListener
{
    [Header("Cost & Currency")]
    [SerializeField] private CurrencyTypes _currencyType;
    [SerializeField] private int _basePrice;
    [SerializeField] private int _priceIncrementPerLevel;

    [Header("Buttons to unlock after purchase")]
    [SerializeField] private List<UpgradePurchaseButtonUI> _unlockedButtons;

    [Header("Upgrade Component")]
    [SerializeField] private MonoBehaviour _upgradeComponent;

    [Header("Availability")]
    [SerializeField] private bool _canPurchase;

    private TextMeshProUGUI _priceText;
    private PlayerCurrency _currency;

    public Button Button { get; private set; }
    public bool CanPurchase { get => _canPurchase; set => _canPurchase = value; }

    private void Awake()
    {
        Button = GetComponent<Button>();
        _currency = FindFirstObjectByType<PlayerCurrency>();
        _priceText = GetComponentInChildren<TextMeshProUGUI>();

        UpdatePriceText();
    }

    public void OnEnable()
    {
        Button.onClick.AddListener(TryPurchase);
        EventBus.Subscribe<OnPlayerGoToUpgradesShop>(InitializeButtons);
    }

    public void OnDisable()
    {
        Button.onClick.RemoveListener(TryPurchase);
        EventBus.Unsubscribe<OnPlayerGoToUpgradesShop>(InitializeButtons);
    }

    private void TryPurchase()
    {
        if (_upgradeComponent is IStatsUpgrade scalableUpgrade)
        {
            TryPurchaseScalableUpgrade(scalableUpgrade);
        }
        else if (_upgradeComponent is IUpgrade simpleUpgrade)
        {
            TryFunctionalSimpleUpgrade(simpleUpgrade);
        }
    }

    private void TryPurchaseScalableUpgrade(IStatsUpgrade upgrade)
    {
        // Проверка, достигнут ли максимальный уровень
        bool isMaxed = upgrade.CurrentLevel >= upgrade.MaxLevel;
        if (isMaxed)
            return;

        int price = CalculateUpgradePrice(upgrade.CurrentLevel);

        // Проверка на наличие нужной валюты
        bool hasEnoughCurrency = _currency.TrySpend(_currencyType, price);
        if (!hasEnoughCurrency)
            return;

        // Попытка применить апгрейд
        bool upgradeApplied = upgrade.TryPurchase();
        if (!upgradeApplied)
            return;

        UpdatePriceText();

        // Блокировка кнопки, если достигнут максимальный уровень
        if (upgrade.CurrentLevel >= upgrade.MaxLevel)
        {
            Button.interactable = false;
        }

        // Разблокировка следующих кнопок, если они есть
        bool hasUnlockedButtons = _unlockedButtons.Count > 0;
        if (hasUnlockedButtons)
        {
            UnlockFollowingButtons();
        }
    }

    private void TryFunctionalSimpleUpgrade(IUpgrade upgrade)
    {
        // Проверка на наличие валюты
        bool hasEnoughCurrency = _currency.TrySpend(_currencyType, _basePrice);

        if (!hasEnoughCurrency)
            return;

        // Активация апгрейда
        upgrade.ActivateUpgrade();

        // Деактивация текущей кнопки
        Button.interactable = false;
        CanPurchase = false;

        // Разблокировка следующих кнопок
        UnlockFollowingButtons();
    }

    private void InitializeButtons(OnPlayerGoToUpgradesShop @event)
    {
        if (CanPurchase && HasEnoughCurrency())
        {
            Button.interactable = true;
        }
    }

    private void UnlockFollowingButtons()
    {
        foreach (var button in _unlockedButtons)
        {
            button.CanPurchase = true;
            button.Button.interactable = button.HasEnoughCurrency();
        }
    }

    private int CalculateUpgradePrice(int level)
    {
        return _basePrice + level * _priceIncrementPerLevel;
    }

    private void UpdatePriceText()
    {
        if (_upgradeComponent is IStatsUpgrade scalableUpgrade)
        {
            int price = CalculateUpgradePrice(scalableUpgrade.CurrentLevel);
            _priceText.text = price.ToString();
        }
        else
        {
            _priceText.text = _basePrice.ToString();
        }
    }

    private bool HasEnoughCurrency()
    {
        int current = GetCurrentCurrency();
        int required = _upgradeComponent is IStatsUpgrade scalable
            ? CalculateUpgradePrice(scalable.CurrentLevel)
            : _basePrice;

        return current >= required;
    }

    private int GetCurrentCurrency()
    {
        return _currencyType switch
        {
            CurrencyTypes.RedCurrency => _currency.RedCurrency,
            CurrencyTypes.YellowCurrency => _currency.YellowCurrency,
            CurrencyTypes.GreenCurrency => _currency.GreenCurrency,
            CurrencyTypes.BlueCurrency => _currency.BlueCurrency,
            _ => 0
        };
    }
}
