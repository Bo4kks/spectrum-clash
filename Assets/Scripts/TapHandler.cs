using UnityEngine;

public class TapHandler : MonoBehaviour, IEventPusher
{
    [SerializeField] private PolygonCollider2D _touchZone;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x,touch.position.y,0f));

                Vector2 touchPos2D = new Vector2(worldPos.x, worldPos.y);

                if (_touchZone.OverlapPoint(touchPos2D))
                {
                    EventBus.Invoke(new OnTouchZonePressedEvent());
                }
            }
        }
    }
}
