using UnityEngine;
using UnityEngine.UI;

public class UpgradesShop : MonoBehaviour, IEventListener
{
    [SerializeField] private CanvasGroup _upgradeShop;
    [SerializeField] private Image _quitButton;

    private CanvasGroupSettings _canvasGroupSettings = new();

    private void Awake()
    {
        _upgradeShop = GetComponent<CanvasGroup>();
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnPlayerGoToUpgradesShop>(InstantShowUpgradesShop);
        EventBus.Subscribe<OnPlayerQuitUpgradeShop>(InstantHideUpgradeShop);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnPlayerGoToUpgradesShop>(InstantShowUpgradesShop);
        EventBus.Unsubscribe<OnPlayerQuitUpgradeShop>(InstantHideUpgradeShop);
    }

    private void InstantShowUpgradesShop(OnPlayerGoToUpgradesShop @event)
    {
        _canvasGroupSettings.SetCanvasGroupSettingsWithException(_upgradeShop, true, _quitButton, true, false);
    }

    private void InstantHideUpgradeShop(OnPlayerQuitUpgradeShop @event) 
    {
        _canvasGroupSettings.SetCanvasGroupSettingsWithException(_upgradeShop, false, _quitButton, true, false);
    }
}
