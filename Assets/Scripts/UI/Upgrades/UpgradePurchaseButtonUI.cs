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

    [Header("UI texts")]
    [SerializeField] private TextMeshProUGUI _upgradelvlText;
    [SerializeField] private TextMeshProUGUI _priceText;

    [Header("Icon")]
    [SerializeField] private Image _costIcon;

    private PlayerCurrency _currency;

    public Button Button { get; private set; }
    public bool CanPurchase { get => _canPurchase; set => _canPurchase = value; }

    private void Awake()
    {
        Button = GetComponent<Button>();
        _currency = FindFirstObjectByType<PlayerCurrency>();

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
        // ��������, ��������� �� ������������ �������
        bool isMaxed = upgrade.CurrentLevel >= upgrade.MaxLevel;
        if (isMaxed)
            return;

        int price = CalculateUpgradePrice(upgrade.CurrentLevel);

        // �������� �� ������� ������ ������
        bool hasEnoughCurrency = _currency.TrySpend(_currencyType, price);
        if (!hasEnoughCurrency)
            return;

        // ������� ��������� �������
        bool upgradeApplied = upgrade.TryPurchase();
        if (!upgradeApplied)
            return;

        UpdatePriceText();
        UpdateLvlText();

        // ���������� ������, ���� ��������� ������������ �������
        if (upgrade.CurrentLevel >= upgrade.MaxLevel)
        {
            Button.interactable = false;
        }

        // ������������� ��������� ������, ���� ��� ����
        UnlockFollowingButtons();
        
    }

    private void TryFunctionalSimpleUpgrade(IUpgrade upgrade)
    {
        // �������� �� ������� ������
        bool hasEnoughCurrency = _currency.TrySpend(_currencyType, _basePrice);

        if (!hasEnoughCurrency)
            return;

        // ��������� ��������
        upgrade.ActivateUpgrade();
        UpdateLvlText();

        // ����������� ������� ������
        Button.interactable = false;
        CanPurchase = false;

        // ������������� ��������� ������
        UnlockFollowingButtons();
    }

    private void InitializeButtons(OnPlayerGoToUpgradesShop @event)
    {
        if (CanPurchase && HasEnoughCurrency())
        {
            if (_upgradeComponent is IStatsUpgrade scalableUpgrade)
            {
                if (scalableUpgrade.CurrentLevel == scalableUpgrade.MaxLevel)
                {
                    return;
                }
            }

            Button.interactable = true;
        }
    }

    private void UnlockFollowingButtons()
    {
        bool hasUnlockedButtons = _unlockedButtons.Count > 0;

        if (hasUnlockedButtons)
        {
            foreach (var button in _unlockedButtons)
            {
                button.CanPurchase = true;
                button.Button.interactable = button.HasEnoughCurrency();
            }
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

    private void UpdateLvlText()
    {
        if (_upgradeComponent is IStatsUpgrade scalableUpgrade)
        {
            if (scalableUpgrade.CurrentLevel == scalableUpgrade.MaxLevel)
            {
                _upgradelvlText.text = "MAX";
                _costIcon.color = new Color(0, 0, 0, 0);
                _priceText.text = "MAXED";
                return;
            }

            _upgradelvlText.text = scalableUpgrade.CurrentLevel.ToString();
        }
        else if (_upgradeComponent is IUpgrade simpleUpgrade)
        {
            _upgradelvlText.text = "MAX";
            _costIcon.color = new Color(0, 0, 0, 0);
            _priceText.text = "MAXED";
        }
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
