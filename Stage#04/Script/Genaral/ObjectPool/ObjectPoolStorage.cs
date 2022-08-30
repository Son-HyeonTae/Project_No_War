using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* 유니티 내부에서 Generic형식을 AddComponent에 사용할 수 없어서 
* 클래스에 상속하는 형태를 거쳐 사용하게 됨
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class ProjectilePool : ObjectPool<Projectile> { }
public class EnemyPool : ObjectPool<Enemy> { }



/**
* ObjectPool은 모두 여기 명시하여 어디서든 접근할 수 있도록 관리
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class ObjectPoolStorage : Singleton<ObjectPoolStorage>
{
    public ObjectPool<Enemy>        Pool_Enemy { get; private set; }
    public ObjectPool<Projectile>   Pool_Projectile { get; private set; }

    private void Awake()
    {
        Pool_Enemy = gameObject.AddComponent<EnemyPool>();
        Pool_Projectile = gameObject.AddComponent<ProjectilePool>();
    }
}
