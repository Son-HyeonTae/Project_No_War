using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RemainText : MonoBehaviour
{
    [SerializeField]
    public PlayerController playerController;
    private TextMeshProUGUI textRemains;
    private bool gameEndFlag;

    private void Awake() {
        textRemains = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        textRemains.text = "X " + playerController.count;
    }

    public void GameClear() {
        if (gameEndFlag == false) {
            GameManager.Instance.LoadStage(()=>{return true;}, GameManager.Instance.CutScene6, 0f);
            gameEndFlag = true;
        }
    }
}
