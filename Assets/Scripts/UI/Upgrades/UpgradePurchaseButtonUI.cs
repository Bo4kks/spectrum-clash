using UnityEngine;
using UnityEngine.UI;

public class UpgradePurchaseButtonUI : MonoBehaviour
{
    [Header("��������� � ������")]
    [SerializeField] private CurrencyTypes _currencyType;
    [SerializeField] private int _price;

    [Header("������ �� �������")]
    [Tooltip("����� ���������, ����������� IUpgradeAction")]
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
            Debug.LogError($"[{name}] {_upgradeComponent.name} �� ��������� IUpgradeAction");
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
            Debug.LogError($"[{name}] �� ������� ��������� �������: ����������� ������ ��� ��������� ��������");
        }

        if (_currency.TrySpend(_currencyType, _price))
        {
            _upgradeAction.ActivateUpgrade();

            _button.interactable = false;

            Debug.Log($"��������� {_upgradeComponent.name} ������� �� {_price} {_currencyType}");
        }
        else
        {
            Debug.Log($"������������ {_currencyType} ��� ������� {_upgradeComponent.name}");
        }
    }
}
