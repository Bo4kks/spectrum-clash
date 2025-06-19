using UnityEngine;

public class UpgradesShop : MonoBehaviour, IEventListener
{
    [SerializeField] private CanvasGroup _upgradeShop;

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
        _upgradeShop.alpha = 1f;
        _upgradeShop.blocksRaycasts = true;
        _upgradeShop.interactable = true;

    }

    private void InstantHideUpgradeShop(OnPlayerQuitUpgradeShop @event) 
    {
        _upgradeShop.alpha = 0f;
        _upgradeShop.blocksRaycasts = false;
        _upgradeShop.interactable = false;
    }
}
