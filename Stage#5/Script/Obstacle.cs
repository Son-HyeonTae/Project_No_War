using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    
    public GameObject Goal;
    private float moveVelocity = 2f;
    private enum LineState
    {
        Line1,
        Line2,
        Line3,
        Line4
    }
    
    private LineState myLineState;
    private Vector2 destination;
    private float time;
    float increaseSpeed = 0.23f;
    float maxSize = 1.6f;
    private Vector2 nowScale;
    private float increaseVec = 0;

    private void Start()
    {
        // Line1
        if(transform.position.x == -1.08f)
        {
            myLineState = LineState.Line1;
        }
        if(transform.position.x == -0.35f)
        {
            myLineState = LineState.Line2;
        }
        if(transform.position.x == 0.44f)
        {
            myLineState = LineState.Line3;
        }
        if(transform.position.x == 1.14f)
        {
            myLineState = LineState.Line4;
        }
        // 이동 위치 찾기
        destination = Goal.transform.Find("" + myLineState).position;
        // Debug.Log("" + myLineState);

    }


    void Update()
    {
        if(!GMScene5.isGameover)
        {
            Move();
            IncreaseSize();
            
        }
        
    }

    private void Move()
    {
        increaseVec += 1f * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, destination, moveVelocity * Time.deltaTime * increaseVec );


    }


    private void IncreaseSize()
    {
        nowScale = transform.localScale;
        if(transform.localScale.x < 0.4f)
        {
            transform.localScale = nowScale + (Vector2.one * Time.deltaTime * increaseSpeed * 6);
        }
        if(transform.localScale.x < maxSize)
        {
            transform.localScale = nowScale + (Vector2.one * Time.deltaTime * increaseSpeed );
        }

    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(other.name == "DeadZone")
        {
            Die();
        }
    }
}
