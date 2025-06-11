using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangerEnemy : Enemy
{
    [SerializeField] private List<Color32> _availableColors;
    [SerializeField] private float _changeColorInterval = 1.5f; 

    private ColorSequence _colorSequence;
    private int _colorIndex = 0;
    private Coroutine _changeColorCoroutine;

    private void Start()
    {
        _colorSequence = FindFirstObjectByType<ColorSequence>();
        _availableColors = _colorSequence.ColorsSequence;
    }

    protected override void Move()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += speed * Time.deltaTime * direction;
    }

    public override void Initialize(EnemyDataSO data)
    {
        base.Initialize(data);

        _changeColorCoroutine = StartCoroutine(nameof(ChangeColorRoutine));
    }

    private IEnumerator ChangeColorRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_changeColorInterval);

            if (_colorIndex >= _availableColors.Count)
            {
                _colorIndex = 0;
            }

            spriteRenderer.color = _availableColors[_colorIndex];
            color = spriteRenderer.color;
            _colorIndex++;
        }
    }

    public override void Kill()
    {
        if (_changeColorCoroutine != null)
        {
            StopCoroutine(_changeColorCoroutine);
            _changeColorCoroutine = null;
        }

        base.Kill();
    }
}
