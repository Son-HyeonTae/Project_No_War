using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Calculation of cool time from the time of registration
/// 
/// Remove from list after cooldown End of cool time depends on the existence of the object in the list
/// </summary>

public class CooltimeQueue : Singleton<CooltimeQueue>
{
    private struct Pair
    {
        public Weapon weapon;
        public float RegisterCooltime;
    }

    private List<Pair> WeaponQueue;
    private Dictionary<string, Weapon> OriginalWeaponDict;

    private void Awake()
    {
        OriginalWeaponDict = new Dictionary<string, Weapon>();
        WeaponQueue = new List<Pair>();
    }

    //-Used to apply a long period of cool time
    public bool CheckWeaponCooltimeIsOver(Weapon weapon)
    {
        if (OriginalWeaponDict.TryGetValue(weapon.name, out var ago))
        {
            Debug.Log(weapon.name + "Cooltime");
            return false;
        }
        return true;
    }

    public bool Add(Weapon weapon)
    {
        if (OriginalWeaponDict.TryGetValue(weapon.name, out var go))
        {
            return false;
        }
        Pair pair = new Pair();
        pair.weapon = weapon;
        pair.RegisterCooltime = GameManager.Instance.GameTime;
        OriginalWeaponDict.Add(weapon.name, weapon);
        WeaponQueue.Add(pair);
        return true;
    }


    private void Update()
    {
        for (int i = 0; i < WeaponQueue.Count; i++)
        {
            if (GameManager.Instance.GameTime - WeaponQueue[i].RegisterCooltime >= WeaponQueue[i].weapon.data.Cooltime.Value)
            {
                OriginalWeaponDict.Remove(WeaponQueue[i].weapon.name);
                WeaponQueue.RemoveAt(i);
            }
        }
    }
    //---------------------------------------------------------------

    //Used to apply a short period of cool time
    public IEnumerator ShortDelayChecker(float delay, Action DelayChecker)
    {
        yield return new WaitForSeconds(delay);
        DelayChecker();
    }
}
