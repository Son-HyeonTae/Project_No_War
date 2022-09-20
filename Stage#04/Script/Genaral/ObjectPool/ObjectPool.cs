using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using KeyType = System.String;



/**
* ObjectPool에 데이터를 등록하기 위한 양식
* Generic클래스로 만들어 모든 타입에 대응
* 
* @제약사항 - 원본Object shoud not null
* @제약사항 - Key shoud not null
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class ObjectPoolRegisterData<T2> where T2 : Behaviour
{
    public const int CAPACITY = 10;
    public const int MAXCAPACITY = 50;
    public const int EXPANSION_AMOUNT = 1;

    public string ID;
    public T2 Prefab;
    public KeyType Key;
    public int Capacity = CAPACITY;
    public int MaxCapacity = MAXCAPACITY;
}


/**
* 객체들을 삭제하지 않고 활/비활성을 통해 관리
* Generic클래스로 만들어 모든 타입에 대응
*
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class ObjectPool<T> : MonoBehaviour where T : Behaviour
{
    private Queue<T> ObjectQueue;
    private Dictionary<T ,T> ObjectData;
    private ObjectPoolRegisterData<T> PoolRegisterData;
    private int SummonItemCount;

    private void Awake()
    {
        ObjectQueue = new Queue<T>();
        ObjectData = new Dictionary<T, T>();

        SummonItemCount = 0;
    }

    void GenerateObject()
    {
        T go = Instantiate(PoolRegisterData.Prefab);
        go.name = PoolRegisterData.Prefab.name;
        ObjectQueue.Enqueue(go);
        ObjectData.Add(go, go);
        go.gameObject.SetActive(false);
    }


    /**
    * 새로운 ObjectPool생성 시 Data복사 및 데이터 생성
    * 
    * @param        ObjectPoolRegisterData<T> Data
    * @return       등록 성공여부(bool)
    * @Exception
    */
    public bool Register(ObjectPoolRegisterData<T> Data)
    {
        //If aleady exist PoolObject
//        ObjectData.TryGetValue(Data.Key, out var value);
        PoolRegisterData = Data;
        //pre-instancing by capacity
        for (int i = 0; i < Data.Capacity; i++)
        {
            GenerateObject();
        }
        return true;
    }


    /**
    * 최대 생성 갯수를 넘지 않는 선에서 오브젝트 생성(Activation)
    * 보관중인 객체가 없다면 새로 생성하여 리턴(최대 생성 갯수 미만일 경우)
    * 
    * @param        Vector3 location
    * @param        Quaternion rotation
    * @return       Activation여부
    * @Exception
    */
    public bool Spawn(Vector3 location, Quaternion rotation)
    {
        if (PoolRegisterData == null && ObjectQueue.Count > 0)
            return false;

        //If an object exists in the queue / else
        if (ObjectQueue.Count > 0 && SummonItemCount < PoolRegisterData.MaxCapacity)
        {
            SummonItemCount++;
            GetObject(location, rotation);
            return true;
        }
        else if (ObjectQueue.Count <= 0 && SummonItemCount < PoolRegisterData.MaxCapacity)
        {
            SummonItemCount++;
            GenerateObject();
            GetObject(location, rotation);
            return true;
        }
        else
        {
            //Debug.Log("The capacity of the object pool is already maximum or no object to sponsor.");
            return false;
        }
    }

    /**
    * 컨테이너에서 객체를 가져오는 함수
    * 
    * @param        Vector3 location
    * @param        Quaternion rotation
    * @return       
    * @Exception
    */
    private void GetObject(Vector3 location, Quaternion rotation)
    {
        if (ObjectQueue.Count <= 0)
            return;

        T Go = ObjectQueue.Dequeue();
        Go.transform.position = location;
        Go.transform.rotation = rotation;
        Go.gameObject.SetActive(true);
    }


    /**
    * 생성된 객체를 외부에서 다시 Pool로 넣는 함수
    * Pool에 등록된 정보와 동일한 객체만 회수하며 이외의 객체는 파괴
    * 
    * @param        T DespawnObject
    * @return       등록된 정보라면 회수 및 true 리턴
    * @Exception
    */
    public bool Despawn(T DespawnObject)
    {
        
        if(ObjectData.TryGetValue(DespawnObject, out var result))
        {
            SummonItemCount--;
            ObjectQueue.Enqueue(DespawnObject);
            DespawnObject.gameObject.SetActive(false);
            return true;
        }
        else
        {
            Debug.Log($"Not Register Objectpool {DespawnObject.name}");
        }

        return false;
    }


    public void DestroyAllObject()
    {
        for(int i = 0; i < ObjectQueue.Count; i++)
        {
            Destroy(ObjectQueue.Dequeue());
        }
        ObjectQueue.Clear();
        ObjectQueue = null;

        ObjectData.Clear();
        ObjectData = null;

        PoolRegisterData = null;
    }
}
