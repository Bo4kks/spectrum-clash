using UnityEngine;
using UnityEngine.UI;

public class RestartGameButton : MonoBehaviour, IEventPusher, IEventListener
{
    private Button _restartButton;

    private void Awake()
    {
        _restartButton = GetComponent<Button>();
    }

    public void OnEnable()
    {
        EventBus.Subscribe<OnGameOverPanelFadeComplete>(ActivateButton);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnGameOverPanelFadeComplete>(ActivateButton);
    }

    public void RestartGame()
    {
        _restartButton.interactable = false;
        EventBus.Invoke(new OnGameRestartEvent());
    }

    private void ActivateButton(OnGameOverPanelFadeComplete @event) => _restartButton.interactable = true;
}
