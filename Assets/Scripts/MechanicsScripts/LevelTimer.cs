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
    }

    public void OnDisable()
    {
        EventBus.Unsubscribe<OnGameOverEvent>(GameOver);
    }

    private void Update()
    {
        if (_isTimerActive)
        {
            LevelTime += Time.deltaTime;
        }
    }

    private void GameOver(OnGameOverEvent ev) => _isTimerActive = false;
}
