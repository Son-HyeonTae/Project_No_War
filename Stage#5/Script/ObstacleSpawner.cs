using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private GameObject startPositions;
    // public float spawnTime = 4f;
    // private float lastSpawnTime;
    void Start()
    {
        // lastSpawnTime = 0f;
    }

    public void SpawnObstacle(int line)
    {
        if(!GMScene5.isGameover)
        {
            // 시작 위치 찾기.
            // Find("이름") 함수는 "이름"의 자식 오브젝트들을 찾아서 반환.
            Vector2 startPos = startPositions.transform.Find("StartLine" + line).position;
            Instantiate(obstaclePrefabs[Random.Range(0, 3)], startPos, Quaternion.identity);
        }
    }
}
