using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Gun : MonoBehaviour
{
    private ObjectPoolRegisterData ProjectileRegisterData;
    [SerializeField] private Transform GunHead;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float FireDelay;
    private bool bEndDelay;

    private void Awake()
    {
        bEndDelay = true;
        ProjectileRegisterData = new ObjectPoolRegisterData();
        ProjectileRegisterData.Prefab = projectile;
        ProjectileRegisterData.Key = projectile.name;
        ProjectileRegisterData.Capacity = 10;
        ObjectPoolManager.Instance.Register(ProjectileRegisterData);
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
        ObjectPoolManager.Instance.Spawn(ProjectileRegisterData, GunHead.position, GunHead.rotation);
    }

    private void ChangeDelayFlag()
    {
        bEndDelay = true;
    }
}
