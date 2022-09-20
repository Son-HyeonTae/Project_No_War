using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    // 임시
    public enum Type
    {
        Obstacle,
        Bullet
    }

    public Type tileType;

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
        TileMove();
    }

    private void TileMove()
    {
        // transform.position = Vector2.MoveTowards(transform.position, )
        if(!GMScene5.isGameover)
        {
            tileRigidbody.velocity = tileVelocity * Vector2.down;
        }
        
    }
}
