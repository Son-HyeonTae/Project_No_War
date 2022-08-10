using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float Damage;
    [SerializeField] float Velocity;
    [SerializeField] float HitDist;
    private ObjectPoolRegisterData PoolData;

    private void Awake()
    {
        PoolData = new ObjectPoolRegisterData();
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
                    entity.TakeHit(Damage);
                StartCoroutine(RequestDespawn());
            }
            if (hit.transform.CompareTag("Block"))
            {
                StartCoroutine(RequestDespawn());
            }

        }
    }

    IEnumerator RequestDespawn()
    {
        PoolData.Prefab = gameObject;
        PoolData.Key = name;
        ObjectPoolManager.Instance.Despawn(PoolData);
        yield return null;
    }
}
