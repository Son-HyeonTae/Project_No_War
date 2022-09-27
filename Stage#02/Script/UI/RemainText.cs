using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RemainText : MonoBehaviour
{
    [SerializeField]
    public  PlayerController playerController;
    private TextMeshProUGUI  textRemains;
    
    private bool gameEndFlag;

    private void Awake() {
        textRemains = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        textRemains.text = "X " + playerController.count;

        if(Input.GetKeyDown(KeyCode.F1))
        {
            GameClear();
        }
    }

    public void GameClear() {
        if (gameEndFlag == false) {
            GameManager.Instance.LoadStage(() => { return true; }, GameManager.Instance.CutScene06, 0f);
            gameEndFlag = true;
        }
    }
}
