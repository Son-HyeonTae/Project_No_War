using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EntityData", menuName = "Scriptable Object Asset/EntityData")]
public class EntityData : ScriptableObject
{
    public DATA<float> baseHP = new DATA<float>();
    public DATA<float> HP = new DATA<float>();
    public DATA<float> Velocity = new DATA<float>();

    public bool bDead { get; set; }
}

