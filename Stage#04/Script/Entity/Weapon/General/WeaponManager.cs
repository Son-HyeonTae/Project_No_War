using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Create new objects with registered information in Dictionary


public class WeaponManager : Singleton<WeaponManager>
{
    public List<Weapon> WeaponList;
    private Dictionary<string, Weapon> WeaponData;

    private Weapon SeletedWeapon = null;
    private ShowWeaponPreview WeaponPreview;
    private Vector3 UsePoint;

    private void Awake()
    {
        WeaponPreview = new ShowWeaponPreview();
        WeaponPreview.Init();
        WeaponData = new Dictionary<string, Weapon>();
        foreach (var weapon in WeaponList)
        {
            var Go = Instantiate(weapon, transform);
            Go.name = weapon.name;
            WeaponData.Add(Go.name, Go);
        }
    }

    private void Update()
    {
        SetWeapon();


        if (SeletedWeapon != null)
        {

            if (CooltimeQueue.Instance.CheckWeaponCooltimeIsOver(SeletedWeapon))
            {
                if (SeletedWeapon.data.bImmediateStart == false)
                {
                    UsePoint = WeaponPreview.StartWeaponPreview(SeletedWeapon.data.NoneImmediateSource);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    WeaponPreview.EndWeaponPreview();
                    CooltimeQueue.Instance.Add(SeletedWeapon);
                    TryGetWeapon(SeletedWeapon);
                    SeletedWeapon.data.Amount.Value--;
                    Init();
                }
            }
            else
            {
                SeletedWeapon = null;
            }
        }

    }

    private void Init()
    {
        UsePoint = Vector3.zero;
        SeletedWeapon = null;
    }

    public Weapon GetWeaponInDict(string key)
    {
        WeaponData.TryGetValue(key, out var weapon);
        return weapon;
    }

    private bool RemoveAtList(Weapon weapon)
    {
        bool a = WeaponData.Remove(weapon.name);
        bool b = WeaponList.Remove(weapon);
        return a && b;
    }

    private void SetWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Z) && WeaponData.TryGetValue("Grenade", out var v1))
        {
            SeletedWeapon = v1;
        }
        if (Input.GetKeyDown(KeyCode.X) && WeaponData.TryGetValue("FlashBang", out var v2))
        {
            SeletedWeapon = v2;
        }
        if(SeletedWeapon != null && (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)))
        {
            SeletedWeapon = null;
        }
    }

    private Weapon TryGetWeapon(Weapon W)
    {
        Weapon weapon = null;
        weapon = Instantiate(W);
        weapon.name = W.name;
        weapon.Execute(UsePoint);
        return weapon;
    }
}
