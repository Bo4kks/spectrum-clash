using UnityEngine;
using UnityEngine.UI;

public class GoToUpgradesButton : MonoBehaviour, IEventListener
{
    private Button _goToUpgradesButton;

    private void Awake()
    {
        _goToUpgradesButton = GetComponent<Button>();
    }

    private void Start()
    {
        _goToUpgradesButton.interactable = false;
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnGameOverPanelFadeComplete>(SetButtonInteractableToTrue);
        EventBus.Subscribe<OnGameRestartEvent>(SetButtonInteractableToFalse);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnGameOverPanelFadeComplete>(SetButtonInteractableToTrue);
        EventBus.Unsubscribe<OnGameRestartEvent>(SetButtonInteractableToFalse);
    }

    private void SetButtonInteractableToTrue(OnGameOverPanelFadeComplete @event) => _goToUpgradesButton.interactable = true;

    private void SetButtonInteractableToFalse(OnGameRestartEvent @event) => _goToUpgradesButton.interactable = false;

    public void GoToUpgradeShop()
    {
        EventBus.Invoke(new OnPlayerGoToUpgradesShop());
    }

}
