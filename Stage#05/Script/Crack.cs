using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crack : MonoBehaviour
{



    public GameObject Goal;
    public int crackType = 0; // 1 : crack 1  etc
    private float moveVelocity = 2.5f;
    private enum LineState
    {
        Line1,
        Line2,
        Line3,
        Line4,
        Line5

    }
    
    private LineState myLineState;
    protected Vector2 destination;
    private float time;
    float increaseSpeed = 0.05f;
    float[] maxSize = new float[] { 0.4f, 0.2f, 0.2f };
    private Vector2 nowScale;
    private float increaseVec = 0;

    
    private void Start()
    {
        // Line1
        if(transform.position.x == -0.81f)
        {
            myLineState = LineState.Line1;
        }
        if(transform.position.x == -0.42f)
        {
            myLineState = LineState.Line2;
        }
        if(transform.position.x == -0.12f)
        {
            myLineState = LineState.Line3;
        }
        if(transform.position.x == 0.26f)
        {
            myLineState = LineState.Line4;
        }
        if(transform.position.x == 0.5f)
        {
            myLineState = LineState.Line5;
        }
        // 이동 위치 찾기
        destination = Goal.transform.Find("Crack_end_" + myLineState).position;
        Debug.Log("장애물 위치:"+myLineState);
        Debug.Log("목표 위치:"+destination);


    }


    void Update()
    {
        if(!GMScene5.isGameover)
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
        if(crackType < 2) // crack의 종류가 1일때
        {
            if(transform.localScale.x < 0.32f)
            {
                // 더 빠르게 증가
                transform.localScale = nowScale + (Vector2.one * Time.deltaTime * increaseSpeed * 1.2f);
            }
            else if(transform.localScale.x < maxSize[crackType])
            {
                transform.localScale = nowScale + (Vector2.one * Time.deltaTime * increaseSpeed );
            }
            
        }
        else
        {
            if(transform.localScale.x < 0.195f)
            {
                // 더 빠르게 증가
                transform.localScale = nowScale + (Vector2.one * Time.deltaTime * increaseSpeed * 1.2f);
            }
            else if(transform.localScale.x < maxSize[crackType])
            {
                transform.localScale = nowScale + (Vector2.one * Time.deltaTime * increaseSpeed );
            }
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
