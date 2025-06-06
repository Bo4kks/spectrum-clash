using TMPro;
using UnityEngine;

public class TimeProgressUI : MonoBehaviour
{
    private TextMeshProUGUI _timeText;
    private LevelTimer _levelTimer;

    private void Awake()
    {
        _levelTimer = FindFirstObjectByType<LevelTimer>();
        _timeText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _timeText.text = _levelTimer.LevelTime.ToString("F2");
    }
}
