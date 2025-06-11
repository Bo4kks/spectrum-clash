using System;
using System.Collections;
using UnityEngine;

public class EnemiesDifficulty : MonoBehaviour, IEventListener
{
    [SerializeField] private float _speedMultiplier;
    [SerializeField] private float _damageMultiplier;

    [SerializeField] private float _speedBuffValue;
    [SerializeField] private float _damageBuffValue;

    public float SpeedMultiplier { get { return _speedMultiplier; } }
    public float DamageMultiplier { get { return _damageMultiplier; } }

    public void OnEnable()
    {
        EventBus.Subscribe<OnTimeToBuffEnemies>(BuffEnemy);
    }

    public void OnDisable ()
    {
        EventBus.Unsubscribe<OnTimeToBuffEnemies>(BuffEnemy);
    }

    private void BuffEnemy(OnTimeToBuffEnemies @event)
    {
        _speedMultiplier += _speedBuffValue;
        _damageMultiplier += _damageBuffValue;
    }
}
