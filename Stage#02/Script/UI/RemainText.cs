using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RemainText : MonoBehaviour
{
    [SerializeField]
    public PlayerController playerController;
    private TextMeshProUGUI textRemains;

    private void Awake() {
        textRemains = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        textRemains.text = "X " + playerController.count;
    }

    /**
         * Scene을 별도로 관리하는 클래스를 만들어
         * 해당 부분 주석 처리 후 새로 적용하였음
         * !해당 주석은 확인 후 제거 가능
         * 
         * 변경 사항 : 
         * 
         *     private string nextSceneName;

         *   public void GameClear() {
         *       SceneManager.LoadScene(nextSceneName);
         *       Debug.Log("GameClear - AllDone");
         *   }
         * -->Stage2ClearFlag.cs에서 확인 가능
         * 
         * 주요 사항 : GameManager.cs, CutSceneManager.cs에서 확인
         * 
         * 최종 수정일 : 2022-08-31::01:18
         * 최종 수정자 : 살메
         */

}
