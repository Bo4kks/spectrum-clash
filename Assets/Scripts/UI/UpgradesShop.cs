using UnityEngine;
using UnityEngine.UI;

public class UpgradesShop : MonoBehaviour, IEventListener
{
    [SerializeField] private CanvasGroup _currencyValuePanel;
    [SerializeField] private GameObject _shopScrollArea;
    [SerializeField] private Button _backToGameOverScreenButton;
    [SerializeField] private GameObject _backToGameOverScreenImage;

    private void Awake()
    {
        _backToGameOverScreenButton.interactable = false;
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
        _currencyValuePanel.alpha = 1f;
        _shopScrollArea.SetActive(true);
        _backToGameOverScreenImage.SetActive(true);
        _backToGameOverScreenButton.interactable = true;
    }

    private void InstantHideUpgradeShop(OnPlayerQuitUpgradeShop @event)
    {
        _currencyValuePanel.alpha = 0f;
        _shopScrollArea.SetActive(false);
        _backToGameOverScreenImage.SetActive(false);
        _backToGameOverScreenButton.interactable = false;
    }
}
