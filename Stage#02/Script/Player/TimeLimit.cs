using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLimit : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private float  timeDamage   = 5.0f;
    [SerializeField]
    private float  timeIncrease = 3.0f;
    
    private SpriteRenderer spriteRenderer;
    
    [SerializeField]
    private float  maxTime      = 100f;
    public  float  MaxTime      => maxTime;
    private float  currentTime;
    public  float  CurrentTime  => currentTime;

    private void Awake() {
        currentTime = maxTime;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if      (playerController.count >= 80) currentTime -= 10f * Time.deltaTime;
        else if (playerController.count >= 70) currentTime -= 14f * Time.deltaTime;
        else if (playerController.count >= 60) currentTime -= 18f * Time.deltaTime;
        else if (playerController.count >= 50) currentTime -= 20f * Time.deltaTime;
        else if (playerController.count >= 40) currentTime -= 22f * Time.deltaTime;
        else if (playerController.count >= 30) currentTime -= 24f * Time.deltaTime;
        else if (playerController.count >= 20) currentTime -= 26f * Time.deltaTime;
        else if (playerController.count >= 10) currentTime -= 28f * Time.deltaTime;
        else if (playerController.count >=  0) currentTime -= 29f * Time.deltaTime;

        /**
         * Scene을 별도로 관리하는 클래스를 만들어
         * 해당 부분 주석 처리 후 새로 적용하였음
         * !해당 주석은 확인 후 제거 가능
         * 
         * 변경 사항 : 
         * 
         * [SerializeField] private string nextSceneName;
         * 
         * if (currentTime <= 0) {
                        SceneManager.LoadScene(nextSceneName); 
         * -->Stage2ClearFlag.cs에서 확인 가능
         * 
         * 주요 사항 : GameManager.cs, CutSceneManager.cs에서 확인
         * 
         * 최종 수정일 : 2022-08-31::01:18
         * 최종 수정자 : 살메
         */

    }

    public void ReduceTime() {
        currentTime -= timeDamage;
    }

    public void IncreaseTime() {
        currentTime += timeIncrease;

        if (currentTime >= maxTime) {
            currentTime  = maxTime;
        }
    }
}
