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

    protected override void Awake()
    {
        base.Awake();

        _movePoints = FindObjectsByType<MovePointMarker>(FindObjectsSortMode.None);
    }

    public override void Initialize(EnemyDataSO enemyDataSO)
    {
        base.Initialize(enemyDataSO);

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

        RotateTowards(direction);

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
        RotateTowards(_chargeDirection);
        transform.position += (Vector3)(_chargeSpeedMultiplier * speed * Time.deltaTime * _chargeDirection);
    }

    public override void Kill()
    {
        if (_lineRenderer != null)
            _lineRenderer.enabled = false;

        base.Kill();
    }

    private void RotateTowards(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
    }
}

