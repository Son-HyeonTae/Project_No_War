using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KeyType = System.String;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    //Cannot be duplicated
    private Dictionary<KeyType, ObjectPool> PoolDataDict;

    private void Awake()
    {
        PoolDataDict = new Dictionary<KeyType, ObjectPool>();
    }

    public bool Register(ObjectPoolRegisterData Data)
    {
        if (PoolDataDict.TryGetValue(Data.Key, out var obj))
        {
            return false;
        }
        else
        {
            ObjectPool pool = new ObjectPool(); 
            PoolDataDict.Add(Data.Key, pool);
            pool.Register(Data);
            return true;
        }
    }

    public bool Spawn(ObjectPoolRegisterData Data, Vector3 location, Quaternion rotation)
    {
        if (PoolDataDict.TryGetValue(Data.Key, out ObjectPool obj))
        {
            obj.Spawn(location, rotation);
            return true;
        }
        return false;
    }

    public bool Despawn(ObjectPoolRegisterData Data)
    {
        if (PoolDataDict.TryGetValue(Data.Key, out ObjectPool obj))
        {
            obj.Despawn(Data.Prefab);
            return true;
        }
        Destroy(Data.Prefab);
        return false;
    }
}
