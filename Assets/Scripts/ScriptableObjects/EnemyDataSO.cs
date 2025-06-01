using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _speed;
    [SerializeField] private Color32 _color;


    public GameObject EnemyPrefab { get => _enemyPrefab; }
    public float Speed { get => _speed; }
    public Color32 Color { get => _color; }

}
