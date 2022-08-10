using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���� �ֻ��� Ŭ����
/// �⺻���� ��� ���� �� �� Ŭ������ ���� ���� Ŭ���� Ȯ�� ����
/// 
/// 
/// Class TypeExplosion - Cooltime
/// Class Projectile - FireDelay, Velocity
/// </summary>

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Object Asset/WeaponData")]
public class WeaponData : ScriptableObject
{
    public DATA<float> Damage = new DATA<float>();
    public DATA<float> Cooltime = new DATA<float>();
    public DATA<int> Amount = new DATA<int>();

    public bool bImmediateStart;
    public GameObject NoneImmediateSource;
}

public class Weapon : MonoBehaviour 
{
    [HideInInspector] public WeaponData data { get; protected set; }
    [SerializeField] private GameObject NoneImmediateSource;
    
    public virtual void Init()
    {
        data = ScriptableObject.CreateInstance<WeaponData>();
        data.Damage.Value = 0;
        data.Cooltime.Value = 0;
        data.Amount.Value = 9999;
        data.bImmediateStart = false;
        data.NoneImmediateSource = this.NoneImmediateSource;
    }
    public virtual void Execute(Vector3 coordinate) { }
    public virtual void Execute() { }
}
