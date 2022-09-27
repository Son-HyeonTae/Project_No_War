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
    public static bool isStart;

    [SerializeField] private GameObject[] warningImgs;
    [SerializeField] private GameObject[] haertImgs;
    [SerializeField] private GameObject[] haertBreakImgs;
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private GameObject gameoverUI;

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
        isStart = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(isGameover)
        {
            // 게임 오버이면 게임 오버 UI 활성화
            gameoverUI.SetActive(true);
        }
    }
    
    public void EnableWarning(int line)
    {
        warningImgs[line].SetActive(true);
    }
    public void DisableWarning(int line)
    {
        warningImgs[line].SetActive(false);
    }


    public void BreakHeart(int life)
    {
        haertImgs[(2 - life)].SetActive(false);
        haertBreakImgs[(2 - life)].SetActive(true);
    }

    public void Shake()
    {
        StartCoroutine(cameraShake.Shake(0.15f, 0.3f));
    }

    public void LoadNext() 
    {
        if(!isGameover)
        {
            SceneManager.LoadScene("CutScene11");
        }
    }

    public void ReStart()
    {
        SceneManager.LoadScene("Stage#05");
    }
}

