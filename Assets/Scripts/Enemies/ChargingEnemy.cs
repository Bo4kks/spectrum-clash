using UnityEngine;

public class ChargingEnemy : Enemy
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private float _chargeSpeedMultiplier;

    private Vector2 _chargeDirection;
    [SerializeField] private Transform _movePoint;
    [SerializeField] private bool _hasInitialized;
    [SerializeField] private MovePointMarker[] _movePoints;

    private System.Action _currentState;

    public override void Initialize(EnemyDataSO enemyDataSO)
    {
        base.Initialize(enemyDataSO);

        _movePoints = FindObjectsByType<MovePointMarker>(FindObjectsSortMode.None);

        _movePoint = _movePoints[Random.Range(0, _movePoints.Length)].transform;

        _hasInitialized = true;

        _currentState = MoveToPoint;
    }

    protected override void Move()
    {
        if (!_hasInitialized || _currentState == null) return;

        _currentState.Invoke();
    }

    private void MoveToPoint()
    {
        Vector3 direction = (_movePoint.position - transform.position).normalized;
        transform.position += speed * Time.deltaTime * direction;

        if (Vector2.Distance(transform.position, _movePoint.position) < 0.1f)
        {
            AimAtPlayer();
        }
    }

    private void AimAtPlayer()
    {
        Vector2 playerPos = target.position;
        _chargeDirection = (playerPos - (Vector2)transform.position).normalized;

        if (_lineRenderer != null)
        {
            _lineRenderer.enabled = true;
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, playerPos);
        }

        _currentState = ChargeToPlayer;
    }

    private void ChargeToPlayer()
    {
        transform.position += (Vector3)(_chargeSpeedMultiplier * speed * Time.deltaTime * _chargeDirection);
    }

    public override void ReturnToPool()
    {
        if (_lineRenderer != null)
            _lineRenderer.enabled = false;

        base.ReturnToPool();
    }
}

