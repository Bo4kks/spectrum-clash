using UnityEngine;

public class SimpleEnemy : Enemy
{
    protected override void Move()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
    }
}
