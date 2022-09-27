using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileDetector : MonoBehaviour
{
    [SerializeField] private GameObject[] linePosition;
    [SerializeField] private ObstacleSpawner obstacleSpanwer;
    [SerializeField] private BulletSpawner bulletSpanwer;


    private void OnTriggerEnter2D(Collider2D other)
    {
        float tileX = other.transform.position.x;

        // 충돌 확인
        for(int i = 0; i < 4; i++)
        {
            if(tileX == linePosition[i].transform.position.x && !GMScene5.isGameover)
            {
                int line = i + 1;
                Tile otherTile = other.GetComponent<Tile>();
                
                //Obstacle 타일 (장애물 타일)
                if(otherTile.tileType == 0)
                {  
                    //ObstacleSpawer 호출.
                    obstacleSpanwer.SpawnObstacle(line);
                    
                } // Bullet 타일
                else if(otherTile.tileType == Tile.Type.Bullet)
                {
                    //bulletSpawner 호출
                    bulletSpanwer.SpawnBullet(line);
                }
                else
                {
                    GMScene5.instance.LoadNext();
                }

            }
        }

    }

}


