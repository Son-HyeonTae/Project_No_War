using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidableObject : MonoBehaviour
{
    private Collider2D Collider;
    [SerializeField] private Vector3 HitRange;
    [SerializeField] private Vector3 Offset;
    [SerializeField] private bool bDisplayGizmos;

    public bool bUse { get; private set; }

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Collider2D[] Hit = Physics2D.OverlapBoxAll(transform.position, HitRange, 0);
        if(Hit != null && Hit[0].CompareTag("Enemy"))
        {
            bUse = true;
        }
        else
        {
            bUse = false;
        }
    }

    private void OnDrawGizmos()
    {
        if(bDisplayGizmos)
        {
            Gizmos.DrawWireCube(transform.position + Offset, HitRange);
        }
    }
}
