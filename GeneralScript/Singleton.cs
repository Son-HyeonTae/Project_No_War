using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
* Singleton class
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static bool ShuttingDown;
    private static T instance;

    // Start is called before the first frame update
    /*public void Start()
    {
        DontDestroyOnLoad(gameObject);
    }*/

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

