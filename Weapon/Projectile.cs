using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Weapon
{
    [SerializeField] private float Velocity;

    private void Update()
    {
        Fire();
    }

    public override void Fire()
    {
        transform.position += transform.up * Velocity * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("ObjectDeadLine"))
        {
            Destroy(gameObject);
        }

        if(collision.transform.CompareTag("Enemy"))
        {
            collision.TryGetComponent<Entity>(out var entity);
            if (entity != null)
                entity.TakeHit(Damage);

            Destroy(gameObject);
        }
    }
}
