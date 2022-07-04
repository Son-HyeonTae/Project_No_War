using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : TypeExplosion
{
    public override void Fire()
    {
        base.Fire();
    }

    public override void DecidePosition()
    {
        base.DecidePosition();
        transform.position = new Vector3(0, transform.position.y, 0);
        Image.color = new Color(255, 255, 255, 100);
    }
}
