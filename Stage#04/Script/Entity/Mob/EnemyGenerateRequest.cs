using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerateRequest : MonoBehaviour
{
    private string ABSOLUTE_PATH; //Generate Sequence file path

    private float ElapsedTime;
    public float GenerateTime;
    public GameObject EnemyObject;
    private ObjectPoolRegisterData Data;

    private void Awake()
    {
        ABSOLUTE_PATH = Application.dataPath + "/Stage#4/EnemyGenerateSequence.txt";

        ElapsedTime = 0;

        Data = new ObjectPoolRegisterData();
        Data.Prefab = EnemyObject;
        Data.Key = EnemyObject.name;
        Data.Capacity = 2;
        Data.ExpensionAmount = 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        ObjectPoolManager.Instance.Register(Data);
    }

    // Update is called once per frame
    void Update()
    {
        ElapsedTime += Time.deltaTime;

        if(ElapsedTime >= GenerateTime)
        {
            Vector3 position = new Vector3(Random.Range(-3, 3), 5);
            //int type = GenerateQueue.Dequeue();
            StartCoroutine(GenerateEnemy(position));
            ElapsedTime = 0;
        }
    }

    IEnumerator GenerateEnemy(Vector3 location)
    {
        ObjectPoolManager.Instance.Spawn(Data, location, Quaternion.identity);
        yield return null;
    }
}
