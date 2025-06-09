using DG.Tweening;
using UnityEngine;

public class OscillatorEnemy : Enemy
{
    [SerializeField] private float _oscillationAmplitude = 1f;
    [SerializeField] private float _oscillationDuration = 1f;
    [SerializeField] private float _rotationSpeed = 180f;

    private float _oscillationOffset = 0f;
    private Tween _oscillationTween;

    public override void Initialize(EnemyDataSO data)
    {
        base.Initialize(data);
        StartOscillation();
    }

    protected override void Move()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;

        Vector3 moveVector = direction * speed * Time.deltaTime;

        Vector3 oscillationVector = transform.right * (_oscillationAmplitude * _oscillationOffset);

        transform.position += moveVector + oscillationVector;
    }

    private void StartOscillation()
    {
        _oscillationTween?.Kill();

        _oscillationTween = DOTween.To(() => 0f, x => _oscillationOffset = x, 1f, _oscillationDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }

    public override void ReturnToPool()
    {
        _oscillationTween?.Kill();
        base.ReturnToPool();
    }
}
