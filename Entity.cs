using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamgeable
{
    void TakeHit(float damage);
}
public class Entity : MonoBehaviour, IDamgeable
{
    public float baseHP;
    protected float HP;
    protected bool bDead;

    protected virtual void Start()
    {
        HP = baseHP;
    }
    public void TakeHit(float damage)
    {
        HP -= damage;

        if (HP <= 0 & !bDead)
        {
            Die();
        }
    }

    protected void Die()
    {
        bDead = true;
        GameObject.Destroy(gameObject);
    }
}
