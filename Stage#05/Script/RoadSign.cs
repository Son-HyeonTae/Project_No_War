using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSign : MonoBehaviour
{

    [SerializeField] private GameObject goal;
    private Vector2 destination;
    private float moveVelocity = 3.8f;
    // 크기 조절 변수
    private Vector2 nowScale;
    private float maxSize = 0.43f;
    private float increaseSpeed = 0.048f;


    private void Start() 
    {   
        destination = goal.transform.position;
    }

    void Update()
    {
        if(!GMScene5.isGameover && GMScene5.isStart) 
        {
            Move();
            IncreaseSize(); 
        }
        
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, moveVelocity * Time.deltaTime);
    }

    private void IncreaseSize()
    {
        nowScale = transform.localScale;
        if(transform.localScale.x < 0.4f)
        {
            // 더 빠르게 증가
            transform.localScale = nowScale + (Vector2.one * Time.deltaTime * increaseSpeed * 1.2f);
        }
        if(transform.localScale.x < maxSize)
        {
            transform.localScale = nowScale + (Vector2.one * Time.deltaTime * increaseSpeed );
        }

    }

}
