using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 무기 최상위 클래스
/// 기본적인 기능 구현 및 이 클래스를 통해 무기 클래스 확장 구현
/// 
/// 
/// Class TypeExplosion - Cooltime
/// Class Projectile - FireDelay, Velocity
/// </summary>

public abstract class Weapon : MonoBehaviour
{
    public abstract void Fire(); //무조건 구현
    public virtual void Init() { } //필요에 따라 구현

    //--
    [SerializeField]    public float Damage;
    [SerializeField]    public float Amount;
    [SerializeField]    public float Cooltime;
                        public bool bReady;
}
