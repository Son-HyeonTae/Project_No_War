using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyType = System.String;


/// <summary>
/// 제약사항 : Prefab is not null, Key is Not null
/// </summary>
public class ObjectPoolRegisterData
{
    public const int CAPACITY = 10;
    public const int EXPANSION_AMOUNT = 1;

    public GameObject Prefab;
    public KeyType Key;
    public int Capacity = CAPACITY;
    public int ExpensionAmount = EXPANSION_AMOUNT;
}

public class ObjectPool : MonoBehaviour
{
    private int Capacity;
    private Queue<GameObject> ObjectData;
    ObjectPoolRegisterData Data;

    private void Init(ObjectPoolRegisterData Data)
    {
        this.Data = Data;
        Capacity = Data.Capacity;

        ObjectData = new Queue<GameObject>(Capacity);
    }

    void GenerateObject(ObjectPoolRegisterData Data)
    {
        GameObject go = Instantiate(Data.Prefab);
        go.name = Data.Prefab.name;
        go.gameObject.SetActive(false);
        ObjectData.Enqueue(go);
    }

    public bool Register(ObjectPoolRegisterData Data)
    {
        Init(Data);
        //pre-instancing by capacity
        for (int i = 0; i < Capacity; i++)
        {
            GenerateObject(Data);
        }
        return true;
    }

    public bool Spawn(Vector3 location, Quaternion rotation)
    {
        //If an object exists in the queue / else
        if (ObjectData.Count > 0)
        {
            GetObject(location, rotation);
            return true;
        }
        else if (ObjectData.Count <= 0)
        {
            GenerateObject(Data);
            GetObject(location, rotation);
            return true;
        }
        else
        {
            Debug.Log("The capacity of the object pool is already maximum or no object to sponsor.");
            return false;
        }
    }

    private void GetObject(Vector3 location, Quaternion rotation)
    {
        GameObject Go = ObjectData.Dequeue();
        Go.transform.position = location;
        Go.transform.rotation = rotation;
        Go.gameObject.SetActive(true);
    }

    public bool Despawn(GameObject DespawnObject)
    {
        if (DespawnObject != null)
        {
            ObjectData.Enqueue(DespawnObject);
            DespawnObject.gameObject.SetActive(false);
            return true;
        }
        else
        {
            return false;
        }
    }
}
