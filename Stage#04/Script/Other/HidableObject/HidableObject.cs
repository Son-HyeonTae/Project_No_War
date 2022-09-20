using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidableObject : MonoBehaviour
{
    [SerializeField] private Vector3 Offset;
    [SerializeField] private bool bDisplayGizmos;

    public bool bUse { get; private set; }
    public Vector3 HidableObjectPosition { get; private set; }
    public Entity EnemyInUse { get; private set; }

    private void Start()
    {
        HidableObjectPosition = transform.position + Offset;
    }

    /**
    * 엄폐물을 사용중인 Entity의 정보를 보관하고 상태를 '사용중'으로 전환
    * 
    * 
    * @param (Entity) enemy 현재 엄폐물을 사용중인 Entity의 정보
    * @return NULL
    * @exception 
    */
    public void InUse(Entity enemy)
    {
        bUse = true;
        EnemyInUse = enemy;
    }
    /**
    * Entity정보를 폐기 및 상태를 "미사용"으로 전환
    * 
    * @return NULL
    * @exception 
    */
    public bool UseOut()
    {
        bUse = false;
        EnemyInUse = null;
        return true;
    }

    private void OnDrawGizmos()
    {
        if(bDisplayGizmos)
        {
            Gizmos.DrawSphere(transform.position + Offset, 0.1f);
        }
    }
}
