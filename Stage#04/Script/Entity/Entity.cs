using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDamgeable
{
    void TakeHit(float damage);
}

public class Entity : MonoBehaviour, IDamgeable
{
    public EntityData data { get; set; }

    public virtual void Awake()
    {
        data = ScriptableObject.CreateInstance<EntityData>();
        Init();
    }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        data.baseHP.Value = 100;
        data.HP.Value = data.baseHP.Value;
        data.Velocity.Value = 1;
        data.bDead = false;
    }
    public void TakeHit(float damage)
    {
        data.HP.Value -= damage;
        if (data.HP.Value <= 0 && !data.bDead)
        {
            Die();
        }
    }

    public virtual void HitAction()
    {

    }

    protected void Die()
    {
        data.bDead = true;
    }



}
