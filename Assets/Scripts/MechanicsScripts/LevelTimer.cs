using UnityEngine;

public class LevelTimer : MonoBehaviour, IEventListener
{
    [SerializeField] private float _levelTimer;
    [SerializeField] private bool _isTimerActive = true;

    public float LevelTime
    {
        get
        {
            return _levelTimer;
        }
        private set
        {
            _levelTimer = value;
        }
    }


    public void OnEnable()
    {
        EventBus.Subscribe<OnGameOverEvent>(GameOver);
        EventBus.Subscribe<OnGameRestartEvent>(RestartGame);
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnGameOverEvent>(GameOver);
        EventBus.Unsubscribe<OnGameRestartEvent>(RestartGame);
    }

    private void Update()
    {
        if (_isTimerActive)
        {
            LevelTime += Time.deltaTime;
        }
    }

    private void GameOver(OnGameOverEvent @event) => _isTimerActive = false;

    private void RestartGame(OnGameRestartEvent @event)
    {
        _levelTimer = 0f;
        _isTimerActive = true;
    }
}
