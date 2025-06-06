using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [SerializeField] private float _levelTimer;
    [SerializeField] private bool _isTimerActive;

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

    private void OnEnable()
    {
        _isTimerActive = true;
    }

    private void OnDisable()
    {
        _isTimerActive = false;
    }

    private void Update()
    {
        if (_isTimerActive)
        {
            LevelTime += Time.deltaTime;
        }
    }
}
