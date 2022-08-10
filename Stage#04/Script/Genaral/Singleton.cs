using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// protected MyClassname() {} 을 선언해서 비 싱글톤 생성자 사용을 방지할 것
/// <summary>
/// public class myStaticClass : Singlton<myStaticClass>{
///     protected myStaticClass()
///     {
///     }
/// }
/// </summary>
/// <typeparam name="T"></typeparam>

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool ShuttingDown;
    private static T instance;

    // Start is called before the first frame update
    void Awake()
    {
    }

    public static T Instance
    {
        get
        {
            if (ShuttingDown)
            {
                Debug.Log("[Singleton] Instance '" + typeof(T) + "' already destroyed. Returning null.");
                return null;
            }

            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));

                if(instance == null)
                {
                    var SingletonObject = new GameObject();
                    instance = SingletonObject.AddComponent<T>();
                }
            }
            //DontDestroyOnLoad(instance);
            return instance;
        }
    }

    private void OnApplicationQuit()
    {
        ShuttingDown = true;
    }

    private void OnDestroy()
    {
        ShuttingDown = true;
    }
}

