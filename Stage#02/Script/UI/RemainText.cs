using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class RemainText : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;
    public PlayerController playerController;
    private TextMeshProUGUI textRemains;

    private void Awake() {
        textRemains = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        textRemains.text = "X " + playerController.count;
    }

    public void GameClear() {
        SceneManager.LoadScene(nextSceneName);
        Debug.Log("GameClear - AllDone");
    }
}
