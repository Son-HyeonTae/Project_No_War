using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Gun : MonoBehaviour
{
    private ObjectPoolRegisterData<Projectile> ProjectileRegisterData;
    [SerializeField] private Transform GunHead;
    [SerializeField] private Projectile projectile;
    [SerializeField] private float FireDelay;
    private bool bEndDelay;

    private void Awake()
    {
        bEndDelay = true;
        ProjectileRegisterData              = new ObjectPoolRegisterData<Projectile>();
        ProjectileRegisterData.ID           = "ProjectilePool";
        ProjectileRegisterData.Prefab       = projectile;
        ProjectileRegisterData.Key          = projectile.name;
        ProjectileRegisterData.Capacity     = 10;
        ProjectileRegisterData.MaxCapacity  = 20;

        ObjectPoolStorage.Instance.Pool_Projectile.Register(ProjectileRegisterData); 
    }

    private void Update()
    {
        if(Input.GetKey(KeyCode.Space) && bEndDelay)
        {
            GetProjectile();
            bEndDelay = false;
            StartCoroutine(CooltimeQueue.Instance.ShortDelayChecker(FireDelay, ChangeDelayFlag));
        }
    }

    private void GetProjectile()
    {
        ObjectPoolStorage.Instance.Pool_Projectile.Spawn(GunHead.position, GunHead.rotation);
    }

    private void ChangeDelayFlag()
    {
        bEndDelay = true;
    }
}
