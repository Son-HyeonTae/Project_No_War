using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Enemy생성 시, 최적화를 위해
//추후 ObjectPool구현 필요


public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private EnemyBase enemy;
    private bool bEndCooltime;
    private float Cooltime;
    private int GenerateAmount;

    private void Awake()
    {
        bEndCooltime = true;
        Cooltime = 2.0f;
        GenerateAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        GenerateAmount = (((int)GameManager.Instance.time) / 20) + 1;
        if (bEndCooltime)
        {
            StartCoroutine(GenerateEnemy());
        }

    }

    IEnumerator GenerateEnemy()
    {
        bEndCooltime = false;

        for (int i = 0; i < GenerateAmount; i++)
        {
            Instantiate(enemy, new Vector3(Random.Range(-5, 5), 5), transform.rotation);
        }
        yield return new WaitForSeconds(Cooltime);
        bEndCooltime = true;
    }

}
