using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : EnemyBase
{
    private Vector3 StartPosition;
    private void Awake()
    {
        StartPosition = new Vector3(6.0f, Random.Range(-8, 8));

    }

    // Update is called once per frame
    void Update()
    {
        Execute();
    }
    public override void Execute()
    {
        transform.position += -transform.up * Velocity * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("ObjectDeadLine"))
        {
            //나중에 ObjectPool로 구현
            Destroy(gameObject);
        }
    }
}
