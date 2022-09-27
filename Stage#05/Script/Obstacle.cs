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
    protected Vector2 destination;
    private float time;
    float increaseSpeed = 0.045f;
    float maxSize = 0.19f;
    private Vector2 nowScale;
    private float increaseVec = 0.1f;

    private void Start()
    {
        // Line1
        if(transform.position.x == -1.1f)
        {
            myLineState = LineState.Line1;
        }
        if(transform.position.x == -0.47f)
        {
            myLineState = LineState.Line2;
        }
        if(transform.position.x == 0.18f)
        {
            myLineState = LineState.Line3;
        }
        if(transform.position.x == 0.7f)
        {
            myLineState = LineState.Line4;
        }
        // 이동 위치 찾기
        destination = Goal.transform.Find("End" + myLineState).position;
        Debug.Log("장애물 위치:"+myLineState);
        Debug.Log("목표 위치:"+destination);

    }


    void Update()
    {
        if(!GMScene5.isGameover && GMScene5.isStart)
        {
            Move();
            IncreaseSize();
            
        }
        
    }

    protected void Move()
    {
        increaseVec += 1f * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, destination, moveVelocity * Time.deltaTime * increaseVec );


    }


    private void IncreaseSize()
    {
        nowScale = transform.localScale;
        if(transform.localScale.x < 0.1f)
        {
            // 더 빠르게 증가
            transform.localScale = nowScale + (Vector2.one * Time.deltaTime * increaseSpeed );
        }
        if(transform.localScale.x < maxSize)
        {
            // 더 빠르게 증가
            transform.localScale = nowScale + (Vector2.one * Time.deltaTime * increaseSpeed * 1.5f);
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
