using System;
using System.Collections;
using UnityEngine;

public class EnemiesDifficulty : MonoBehaviour, IEventListener
{
    [SerializeField] private float _speedMultiplier;
    [SerializeField] private float _damageMultiplier;

    [SerializeField] private float _speedBuffValue;
    [SerializeField] private float _damageBuffValue;

    private float _startSpeedMultiplier;
    private float _startDamageMultiplier;

    private float _startSpeedBuffValue;
    private float _startDamageBuffValue;

    private void Start()
    {
        _startSpeedMultiplier = _speedMultiplier;
        _startDamageMultiplier = _damageMultiplier;
        _startSpeedBuffValue = _speedBuffValue;
        _startDamageBuffValue = _damageBuffValue;
    }

    public float SpeedMultiplier { get { return _speedMultiplier; } }
    public float DamageMultiplier { get { return _damageMultiplier; } }

    public void OnEnable()
    {
        EventBus.Subscribe<OnTimeToBuffEnemies>(BuffEnemy);
        EventBus.Subscribe<OnGameRestartEvent>(UpdateFields);
    }

    public void OnDisable ()
    {
        EventBus.Unsubscribe<OnTimeToBuffEnemies>(BuffEnemy);
        EventBus.Unsubscribe<OnGameRestartEvent>(UpdateFields);
    }

    private void UpdateFields(OnGameRestartEvent ev)
    {
        _speedMultiplier = _startSpeedMultiplier;
        _damageMultiplier = _startDamageMultiplier;
        _damageBuffValue = _startDamageBuffValue;
        _speedBuffValue = _startSpeedBuffValue;
    }

    private void BuffEnemy(OnTimeToBuffEnemies @event)
    {
        _speedMultiplier += _speedBuffValue;
        _damageMultiplier += _damageBuffValue;
    }
}
