using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Game/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private Color32 _color;
    [SerializeField] private float _damage;
    [SerializeField] private CurrencyTypes _currencyType;

    public float Speed { get => _speed; }
    public Color32 Color { get => _color; }
    public float Damage { get => _damage; }
    public CurrencyTypes CurrencyType { get => _currencyType; }

}
