using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float Damage;
    [SerializeField] private float Velocity;
    [SerializeField] private float HitDist;
    private Projectile Self;
    private ObjectPoolRegisterData<Projectile> PoolData;

    private void Awake()
    {
        PoolData    = new ObjectPoolRegisterData<Projectile>();
        Self        = GetComponent<Projectile>();  
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += (transform.up) * Velocity * Time.deltaTime;
        RayCast();
    }

    private void RayCast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, HitDist);
        if (hit)
        {
            if(hit.transform.TryGetComponent<Entity>(out var entity))
            {
                if(entity)
                    entity.TakeHit(Damage, eEntityHitType.STIFF);
                RequestDespawn();
            }
            if (hit.transform.CompareTag("Block"))
            {
                RequestDespawn();
            }

        }
    }

    private void RequestDespawn()
    {
        ObjectPoolStorage.Instance.Pool_Projectile.Despawn(this);
    }
}
