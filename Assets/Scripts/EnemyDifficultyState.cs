using System;
using UnityEngine;

public class EnemyDifficultyState : MonoBehaviour, IEventListener
{
    public void OnEnable()
    {
        EventBus.Subscribe<OnEnemiesDifficultyMultiplierEvent>(ChangeValues);
    }


    public void OnDisable()
    {
        EventBus.Unsubscribe<OnEnemiesDifficultyMultiplierEvent>(ChangeValues);
    }

    private void ChangeValues(OnEnemiesDifficultyMultiplierEvent @event)
    {
        throw new NotImplementedException();
    }
}
