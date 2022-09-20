using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Enemy를 규칙에 따라 생성하기 위해 작성된 클래스임
* 파일 입출력을 통해 생성 데이터를 관리
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class EnemyGenerateRequest : Singleton<EnemyGenerateRequest>
{
    private string ABSOLUTE_PATH;                           //Generate Sequence file path

    private float ElapsedTime;
    public  float GenerateTime;
    public  int Capacity;
    public  int MaxCapacity;
    private ObjectPoolRegisterData<Enemy> RegisterData;

    public Enemy EnemyObject;

    private void Awake()
    {
        ABSOLUTE_PATH = Application.dataPath + "/Stage#4/EnemyGenerateSequence.txt";

        ElapsedTime = 0;

        RegisterData                        = new ObjectPoolRegisterData<Enemy>();
        RegisterData.ID                     = "EnemyPool";
        RegisterData.Prefab                 = EnemyObject;
        RegisterData.Key                    = EnemyObject.name;
        RegisterData.Capacity               = Capacity;
        RegisterData.MaxCapacity            = MaxCapacity;

        ObjectPoolStorage.Instance.Pool_Enemy.Register(RegisterData);
    }

    private void Update()
    {
        if (!GameManager.Instance.bLoadedScene)
        {
            Debug.Log("Don't Load Scene");
            return;
        }

        ElapsedTime += Time.deltaTime;

        if(ElapsedTime >= GenerateTime)
        {
            Vector3 position = new Vector3(Random.Range(-3, 3), 7);
            //int type = GenerateQueue.Dequeue();
            StartCoroutine(GenerateEnemy(position));
            ElapsedTime = 0;
        }
    }

    IEnumerator GenerateEnemy(Vector3 location)
    {
        ObjectPoolStorage.Instance.Pool_Enemy.Spawn(location, Quaternion.identity);
        yield return null;
    }
}
