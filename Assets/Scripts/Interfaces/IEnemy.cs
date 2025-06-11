using UnityEngine;

public interface IEnemy
{
    public Color32 Color { get; }
    public void Initialize(EnemyDataSO data);
    public void OnHitPlayer();
    public void Kill();
}
