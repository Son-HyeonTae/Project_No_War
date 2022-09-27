using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // 임시
    public enum Type
    {
        Obstacle,
        Bullet,
        End
    }

    [HideInInspector] public Type tileType;

    [SerializeField] private float tileVelocity;

    private Rigidbody2D tileRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        tileRigidbody = GetComponent<Rigidbody2D>();   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!GMScene5.isGameover && GMScene5.isStart)
        {

            TileMove();
        }
        
    }

    private void TileMove()
    {
        tileRigidbody.velocity = tileVelocity * Vector2.down; 
    }
}
