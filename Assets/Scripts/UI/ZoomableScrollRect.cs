using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ZoomableScrollRect : MonoBehaviour
{
    [Header("Zoom settings")]
    [Tooltip("Максимальный масштаб")]
    [SerializeField] private float _maxScale = 2f;
    [Tooltip("Дополнительный множитель минимального зума (0…1), чем ниже, тем дальше отдаляется")]
    [Range(0.1f, 1f)]
    [SerializeField] private float _minScaleMultiplier = 0.6f;
    [Tooltip("Скорость зума")]
    [SerializeField] private float _zoomSpeed = 0.1f;

    private float _minScale;
    private ScrollRect _scrollRect;
    private RectTransform _viewport;
    private RectTransform _content;

    private Vector2 _prevTouch0;
    private Vector2 _prevTouch1;
    private bool _isPinching;
    private Coroutine _zoomCoroutine;

    private void Awake()
    {
        _scrollRect = GetComponent<ScrollRect>();
        _viewport = _scrollRect.viewport;
        _content = _scrollRect.content;

        Vector2 vpSize = _viewport.rect.size;
        Vector2 cntSize = _content.rect.size;
        float fitW = vpSize.x / cntSize.x;
        float fitH = vpSize.y / cntSize.y;
        float fillScale = Mathf.Max(fitW, fitH, 1f);

        _minScale = fillScale * _minScaleMultiplier;

        _content.localScale = Vector3.one * _minScale;
        _content.pivot = new Vector2(0.5f, 0.5f);
        _content.anchoredPosition = Vector2.zero;
        ClampContentPosition();
    }

    private void Update()
    {
        HandleMouseZoom();
        HandleTouchZoom();
    }

    private void HandleMouseZoom()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        float scrollDelta = Input.mouseScrollDelta.y;
        if (Mathf.Abs(scrollDelta) > 0.01f)
        {
            Zoom(scrollDelta * _zoomSpeed);
        }
#endif
    }

    private void HandleTouchZoom()
    {
        if (Input.touchCount == 2)
        {
            var t0 = Input.GetTouch(0);
            var t1 = Input.GetTouch(1);

            if (!_isPinching)
            {
                _prevTouch0 = t0.position;
                _prevTouch1 = t1.position;
                _isPinching = true;
                return;
            }

            float prevDist = Vector2.Distance(_prevTouch0, _prevTouch1);
            float currDist = Vector2.Distance(t0.position, t1.position);
            float delta = (currDist - prevDist) / Screen.dpi;

            Zoom(delta * _zoomSpeed);

            _prevTouch0 = t0.position;
            _prevTouch1 = t1.position;
        }
        else
        {
            _isPinching = false;
        }
    }

    private void Zoom(float deltaScale)
    {
        float currentScale = _content.localScale.x;
        float targetScale = Mathf.Clamp(currentScale + deltaScale, _minScale, _maxScale);
        if (Mathf.Approximately(currentScale, targetScale))
            return;

        if (_zoomCoroutine != null)
            StopCoroutine(_zoomCoroutine);

        _zoomCoroutine = StartCoroutine(SmoothZoomCoroutine(currentScale, targetScale));
    }

    private IEnumerator SmoothZoomCoroutine(float fromScale, float toScale)
    {
        float duration = 0.2f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
            float curScale = Mathf.Lerp(fromScale, toScale, t);
            _content.localScale = Vector3.one * curScale;
            ClampContentPosition();
            yield return null;
        }

        _content.localScale = Vector3.one * toScale;
        ClampContentPosition();
        _zoomCoroutine = null;
    }

    private void ClampContentPosition()
    {
        Vector2 vpSize = _viewport.rect.size;
        Vector2 cntSize = _content.rect.size * _content.localScale.x;
        Vector2 pos = _content.anchoredPosition;

        if (cntSize.x <= vpSize.x)
            pos.x = 0f;
        else
        {
            float halfDiffX = (cntSize.x - vpSize.x) * 0.5f;
            pos.x = Mathf.Clamp(pos.x, -halfDiffX, halfDiffX);
        }

        if (cntSize.y <= vpSize.y)
            pos.y = 0f;
        else
        {
            float halfDiffY = (cntSize.y - vpSize.y) * 0.5f;
            pos.y = Mathf.Clamp(pos.y, -halfDiffY, halfDiffY);
        }

        _content.anchoredPosition = pos;
    }
}
