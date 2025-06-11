using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IEnemy>(out var enemy))
        {
            if (enemy.Color == _spriteRenderer.color)
            {
                enemy.Kill();
            }
            else
            {
                enemy.OnHitPlayer(); 
            }
        }
        else
        {
            Debug.Log("Collider doesn't implement IEnemy");
        }
    }
}

