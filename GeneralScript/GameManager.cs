using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
/// <summary>
/// 모든 Scene과 게임 전체를 관리하는 클래스
/// 스테이지 시작, 끝에 수행할 요청 처리 및 Scene 전환
/// GameTime 제공(스테이지를 넘어가면 초기화)
/// </summary>


public class GameManager : Singleton<GameManager>
{

    ///======================================
    ///       CutScene Name
    ///======================================
    public string CutScene00 { get; private set; } = "CutScene00";
    public string CutScene01 { get; private set; } = "CutScene01";
    public string CutScene02 { get; private set; } = "CutScene02";
    public string CutScene03 { get; private set; } = "CutScene03";
    public string CutScene04 { get; private set; } = "CutScene04__EB";
    public string CutScene05 { get; private set; } = "CutScene05";
    public string CutScene06 { get; private set; } = "CutScene06__EB";
    public string CutScene07 { get; private set; } = "CutScene07__EB";
    public string CutScene08 { get; private set; } = "CutScene08__EB";
    public string CutScene09 { get; private set; } = "CutScene09";
    public string CutScene10 { get; private set; } = "CutScene10";
    public string CutScene11 { get; private set; } = "CutScene11";

    public string Stage01    { get; private set; } = "Stage#01";
    public string Stage02    { get; private set; } = "Stage#02";
    public string Tutorial02 { get; private set; } = "Stage#02Tutorial";
    public string Stage03    { get; private set; } = "Stage#03";
    public string Stage04    { get; private set; } = "Stage#04";
    public string Stage05    { get; private set; } = "Stage#05";
    ///======================================




    public float GameTime { get; private set; }
    public static Texture2D CursorTexture { get; private set; }

    public bool bLoadedScene { get; set; }

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
    /**
    * Update GameTime
    *
    * @param
    * @return NULL
    * @exception 
    */
    private void Update()
    {
/*        Debug.Log(GameManager.Instance.bLoadedScene);
*/        GameTime += Time.deltaTime;
    }


   /**
   * 조건 충족 여부에 따라 씬 로드
   *
   * @param Func<bool> -callback Condition 씬 로드 조건
   * @param string SceneName 로드될 씬의 이름
   * @param float Delay 로드 되기 전 대기시간
   * @return NULL
   * @exception 
   */
    public void LoadStage(Func<bool> Condition, string SceneName, float Delay = 0/*Don't use*/)
    {
        if(Condition())
        {
       
            StartCoroutine(WaitFor(Delay, SceneName));
        }
    }

    private IEnumerator WaitFor(float Delay, string name)
    {
        yield return new WaitForSeconds(Delay);
        CutSceneManager.Instance.LoadScene(name);

    }

    /**
   * Change Cursor Icon
   *
   * @param 
   * @return NULL
   * @exception 
   */
    public void SetCustomCursor()
    {
        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }


    /**
    * FadeIn/Out Utility
    * 
    * @param (float) FadeTime 변화할 시간
    * @param (float) WaitForSeconds 페이드 전 대기시간
    * @param (Imgage) FadeImage 페이드될 이미지
    * @param (Action)-callback AfterAction 페이드가 완료된 직후 실행할 기능(함수), nullable
    * @return NULL
    * @exception 
    */
    public IEnumerator FadeIn(float FadeTime, float WaitForSeconds, Image FadeImage, Action AfeterAction = null)
    {
        yield return new WaitForSeconds(WaitForSeconds);
        Color color = FadeImage.color;
        color.a = 1.0f;
        while (color.a > 0.0f)
        {
            color.a -= Time.deltaTime / FadeTime;
            FadeImage.color = color;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if (AfeterAction != null) AfeterAction();
    }

    public IEnumerator FadeOut(float FadeTime, float WaitForSeconds, Image FadeImage, Action AfeterAction = null)
    {
        yield return new WaitForSeconds(WaitForSeconds);
        Color color = FadeImage.color;
        color.a = 0.0f;
        while (color.a < 1.0f)
        {
            color.a += Time.deltaTime / FadeTime;
            FadeImage.color = color;
            yield return new WaitForSeconds(Time.deltaTime);
        }

        if (AfeterAction != null) AfeterAction();
    }
}
