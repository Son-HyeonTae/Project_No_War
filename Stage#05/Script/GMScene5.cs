using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GMScene5 : MonoBehaviour
{
    // GameeManager는 싱글턴으로 구현.
    public static GMScene5 instance;
    public static bool isGameover;

    [SerializeField] private GameObject[] warningImgs;
    [SerializeField] private GameObject[] haertImgs;
    [SerializeField] private CameraShake cameraShake;

    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("씬에 두 개 이상의 게임 매니저 존재...");
            Destroy(gameObject);
        }
    }
    void Start()
    {
        isGameover = false;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableWarning(int line)
    {
        warningImgs[line].SetActive(true);
    }
    public void DisableWarning(int line)
    {
        warningImgs[line].SetActive(false);
    }
    public void DisableHeart(int life)
    {
        haertImgs[(2 - life)].SetActive(false);
    }

    public void Shake()
    {
        StartCoroutine(cameraShake.Shake(0.15f, 0.3f));
    }
}

