using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All movable objects other than the player

public class Mob : Entity
{

    //--
    [SerializeField] private ObjectHpUI HpUIObject;

    public override void Awake()
    {
        base.Awake();
    }

    public virtual void Start()
    {
        var ui = Instantiate(HpUIObject);
        ui.Attachment(this, new Vector3(0, -.2f), data);

    }
}
