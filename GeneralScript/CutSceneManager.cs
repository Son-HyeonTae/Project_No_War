using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


/**
* CutScene을 포함한 모든 Scene의 이동을 관리하는 클래스
* 씬의 퇴장과 입장에 Fade효과 구현
* 
* @최종 수정자 - 살메
* @최종 수정일 - 2022-08-25::15:14
*/
public class CutSceneManager : Singleton<CutSceneManager>
{
    public int GetCurrentSceneIndex { get; private set; }
    public Scene GetCurrentScene { get; private set; }
    public bool bLoadedScene { get; private set; } = true;

    public Image FadeImage { get; private set; }
    public Image EyeImage { get; private set; }
    [SerializeField] public Animator EyeAnim;
    private string LoadSceneName;

    [SerializeField] private bool DebugMode;

    private Canvas CanvasSelf;
    private const int BASE_CANVAS_SORTING_ORDER = 999;

    private void Awake()
    {
        GetCurrentSceneIndex = 0;
        DontDestroyOnLoad(transform.parent);
        LoadSceneName = "";

        CanvasSelf = transform.parent.GetComponent<Canvas>();
        CanvasSelf.sortingOrder = BASE_CANVAS_SORTING_ORDER;

        FadeImage = GameObject.Find("Fade").GetComponent<Image>();
        FadeImage.color = new Color(0, 0, 0, 0);
        EyeImage = GameObject.Find("Eye").GetComponent <Image>();
        EyeImage.color = new Color(255, 255, 255, 0);
    }

    #region LoadSceneFunc
    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        OnSceneLoaded();
    }
    private void OnSceneLoaded()
    {
        GameManager.Instance.bLoadedScene = false;

        if (LoadSceneName.Contains("__EB"))
        {
            EyeAnim.Rebind();
            EyeImage.color = Color.white;
            FadeImage.color = Color.black;
            StartCoroutine(GameManager.Instance.FadeIn(0.5f, 0.0f, FadeImage));
            StartCoroutine(
                  GameManager.Instance.FadeIn(1.0f, 0.5f, EyeImage,
                       () => {
                           //EyeImage.color = new Color(255,255,255,0);
                           GameManager.Instance.bLoadedScene = true;
                           CanvasSelf.sortingOrder = -1;
                       }
                  )
              );
        }
        else
        {
            FadeImage.color = Color.black;
            EyeImage.color = new Color(255,255,255,0);
            StartCoroutine(
                  GameManager.Instance.FadeIn(1.0f, 0.5f, FadeImage,
                       () => {
                           //EyeImage.color = new Color(0,0,0, 0);
                           GameManager.Instance.bLoadedScene = true;
                           CanvasSelf.sortingOrder = -1;
                       }
                  )
              );
        }
    }

    public void LoadScene(string Name)
    {
        GameManager.Instance.bLoadedScene = false;
        CanvasSelf.sortingOrder = BASE_CANVAS_SORTING_ORDER;

        LoadSceneName = Name;
        StartCoroutine(GameManager.Instance.FadeOut(1.0f, 1.0f, FadeImage,
             () =>
             {

                 SceneManager.LoadScene(Name);
                 GameManager.Instance.bLoadedScene = true;
                 GetCurrentSceneIndex = SceneManager.sceneCount;
                 GetCurrentScene = SceneManager.GetSceneByName(Name);
             }
             ));

    }
    #endregion

    private void Update()
    {
        if(DebugMode)
        {
            Debug.Log(bLoadedScene);
        }
    }
}
