using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ColorChangerEnemy : Enemy
{
    [SerializeField] private List<Color32> _availableColors;
    [SerializeField] private float _changeColorInterval = 1.5f; 

    private ColorSequence _colorSequence;
    private int _colorIndex = 0;
    private Coroutine _changeColorCoroutine;
    private Color32 _currentColor;

    private Color32 _redColor;
    private Color32 _yellowColor;

    private void Start()
    {
        _colorSequence = FindFirstObjectByType<ColorSequence>();
        _availableColors = _colorSequence.ColorsSequence;

        _redColor = _availableColors[0];
        _yellowColor = _availableColors[1];
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

    protected override void GivePlayerCurrency()
    {
        if (_currentColor.Equals(_redColor))
        {
            EventBus.Invoke(new OnPlayerEarnedCurrencyEvent(CurrencyTypes.RedCurrency));
        }
        else if (_currentColor.Equals(_yellowColor))
        {
            EventBus.Invoke(new OnPlayerEarnedCurrencyEvent(CurrencyTypes.YellowCurrency));
        }
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
            _currentColor = color;
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
