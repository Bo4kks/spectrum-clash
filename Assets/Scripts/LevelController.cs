using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] private LevelDataSO _levelData;
    private int _levelIndex;
    private float _levelDurationIsSeconds;
    private float _timeIntervalToBuffEnemies;
    private float _damageMultiplier;
    private float _speedMultiplier;
    private EnemyDataSO[] _enemyTypes;
    private bool _isInitialized = false;

    public bool IsInitialized
    {
        get
        {
            return _isInitialized;
        }
        private set
        {
            _isInitialized = value;

            if (value)
            {
                
            }
        }
    }
    public float TimeIntervalToBuffEnemies { get { return _timeIntervalToBuffEnemies; } }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _levelIndex = _levelData.LevelIndex;
        _levelDurationIsSeconds = _levelData.LevelDurationIsSeconds;
        _timeIntervalToBuffEnemies = _levelData.TimeIntervalToBufEnemies;
        _enemyTypes = _levelData.EnemyTypes;
        IsInitialized = true;
    }
}
