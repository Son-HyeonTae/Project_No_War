using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/**
* 플레이어 무기 중, 총기 구현 클래스
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-30::15:48
*/
public class Gun : MonoBehaviour
{
    ///===================================================
    ///      Object pool data Variables
    ///===================================================
    private ObjectPoolRegisterData<Projectile> ProjectileRegisterData;


    ///===================================================
    ///      Fire function Variables
    ///===================================================
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
        if (!GameManager.Instance.bLoadedScene)
        {
            Debug.Log("Don't Load Scene");
            return;
        }


        if (Input.GetKey(KeyCode.Space) && bEndDelay)
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
