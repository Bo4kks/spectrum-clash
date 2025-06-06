using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData")]  
public class LevelDataSO : ScriptableObject
{
    [SerializeField] private int _levelIndex;
    [SerializeField] private float _levelDurationIsSeconds;
    [SerializeField] private float _timeIntervalToBufEnemies;
    [SerializeField] private EnemyDataSO[] _enemyTypes;

    public int LevelIndex { get {  return _levelIndex; } }
    public float LevelDurationIsSeconds { get { return _levelDurationIsSeconds; } }
    public float TimeIntervalToBufEnemies { get { return _timeIntervalToBufEnemies; } }
    public EnemyDataSO[] EnemyTypes { get { return _enemyTypes; } }
}
