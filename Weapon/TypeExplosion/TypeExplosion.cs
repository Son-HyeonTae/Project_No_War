using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 생성 시 Collider off, 폭발 대기 시간 이후 on
/// </summary>

public class TypeExplosion : Weapon
{
    [SerializeField] protected float ExplosionDelay;
    protected SpriteRenderer Image;
    protected BoxCollider2D ExplosionAreaCollider;
    protected Collider2D[] TagObjects;
    protected Vector3 DecidedPosition;
    protected bool bDecid;

    private void Awake()
    {
        DecidedPosition = Vector3.zero;
        ExplosionAreaCollider = GetComponent<BoxCollider2D>();
        Image = GetComponent<SpriteRenderer>();
        ExplosionAreaCollider.enabled = false;
        TagObjects = null;
        bDecid = false;
    }

    public override void Fire()
    {
        if(!bDecid)
        {
            DecidePosition();
        }
        if(bDecid)
        {
            Use();
        }
    }

    void Use()
    {
        StartCoroutine(Explosion());

        if (ExplosionAreaCollider.enabled == true)
        {
            foreach (var obj in TagObjects)
            {
                Debug.Log(obj.name);
                //Entity인지 확인
                obj.TryGetComponent<Entity>(out var entity);
                if (entity != null)
                {
                    entity.TakeHit(Damage);
                }
            }

            WeaponManager.Instance.RemoveAtCopyList(this);
            Destroy(gameObject);
        }
        
    }

    public virtual void DecidePosition()
    {
        if (Input.GetMouseButtonDown(1))
        {
            WeaponManager.Instance.RemoveAtCopyList(this);
            Destroy(gameObject);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Image.color = new Color(255, 255, 255, 255);
            DecidedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DecidedPosition.z = 0;
            transform.position = DecidedPosition;
            bDecid = true;
        }
        else
        {
            var p = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(p.x, p.y, 0);

        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(ExplosionDelay);
        ExplosionAreaCollider.enabled = true;
        TagObjects = Physics2D.OverlapBoxAll(transform.position, ExplosionAreaCollider.size, 0);
    }
}
