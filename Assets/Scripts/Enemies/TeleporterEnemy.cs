using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TeleporterEnemy : Enemy
{
    private TeleporterMovePointMarker[] _teleporterMovePoints;
    private TeleporterMovePointMarker[] _teleportSequence;

    private int _currentTeleportIndex = 0;
    private int _teleportCount = 0;
    private float _baseScale;

    private enum State
    {
        Teleporting,
        MovingToPlayer
    }

    private State _currentState;

    [SerializeField] private float _teleportDuration = 0.3f;
    [SerializeField] private float _waitBetweenTeleports = 0.8f;

    protected override void Awake()
    {
        base.Awake();

        _teleporterMovePoints = FindObjectsByType<TeleporterMovePointMarker>(FindObjectsSortMode.None);

        _baseScale = transform.localScale.x;
    }

    public override void Initialize(EnemyDataSO data)
    {
        base.Initialize(data);

        _teleportCount = Random.Range(1, 7);
        _teleportSequence = new TeleporterMovePointMarker[_teleportCount];

        for (int i = 0; i < _teleportCount; i++)
        {
            _teleportSequence[i] = _teleporterMovePoints[Random.Range(0, _teleporterMovePoints.Length)];
        }

        _currentTeleportIndex = 0;
        _currentState = State.Teleporting;

        StartCoroutine(TeleportState());
    }

    protected override void Move()
    {
        if (_currentState == State.MovingToPlayer)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            transform.position += speed * Time.deltaTime * direction;
        }
    }

    private IEnumerator TeleportState()
    {
        while (_currentTeleportIndex < _teleportCount)
        {
            yield return transform.DOScale(0f, _teleportDuration).SetEase(Ease.InBack).WaitForCompletion();

            transform.position = _teleportSequence[_currentTeleportIndex].transform.position;

            yield return transform.DOScale(_baseScale, _teleportDuration).SetEase(Ease.OutBack).WaitForCompletion();

            _currentTeleportIndex++;

            yield return new WaitForSeconds(_waitBetweenTeleports);
        }

        _currentState = State.MovingToPlayer;
    }
}
