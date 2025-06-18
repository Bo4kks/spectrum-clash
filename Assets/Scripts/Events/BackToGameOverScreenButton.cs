using UnityEngine;

public class BackToGameOverScreenButton : MonoBehaviour
{
    public void BackToGameOverScreen() => EventBus.Invoke(new OnPlayerQuitUpgradeShop());
}
