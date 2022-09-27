using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackSpawner : MonoBehaviour
{
   [SerializeField] private GameObject[] crackPrefabs;
    [SerializeField] private GameObject startPositions;
    public float spawnTime = 3f;
    private float lastSpawnTime;
    private void Update() 
    {
        if(Time.time - lastSpawnTime > spawnTime)
        {
            int v = Random.Range(0, 3);
            if(v < 2)
            {
                // SpawnCrack(Random.Range(1, 6));
                SpawnCrack(2);
            }
            else
            {
                // SpawnCrack(Random.Range(1, 6));
                // SpawnCrack(Random.Range(1, 6));
                SpawnCrack(2);
            }
            lastSpawnTime = Time.time;
            
        }
    }

    public void SpawnCrack(int line)
    {
        if(!GMScene5.isGameover)
        {
            // 시작 위치 찾기.
            // Find("이름") 함수는 "이름"의 자식 오브젝트들을 찾아서 반환.
            Vector2 startPos = startPositions.transform.Find("Crack_start_" + line).position;
            
            // crack 랜덤해서 생성
            int r = Random.Range(0, 3);
            GameObject ins = Instantiate(crackPrefabs[r], startPos, Quaternion.identity);
            Crack cr = ins.GetComponent<Crack>();
            cr.crackType = r;
        }
        
    }
}
