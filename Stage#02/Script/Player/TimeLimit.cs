using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLimit : MonoBehaviour
{
    [SerializeField]
    private float maxTime = 100;
    [SerializeField]
    private string nextSceneName;
    [SerializeField]
    private PlayerController playerController;
    private float currentTime;
    private SpriteRenderer spriteRenderer;

    public float MaxTime => maxTime;
    public float CurrentTime => currentTime;

    private void Awake() {
        currentTime = maxTime;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    private void Update() {
        if      (playerController.count >= 80) currentTime -= 12f * Time.deltaTime;
        else if (playerController.count >= 70) currentTime -= 14f * Time.deltaTime;
        else if (playerController.count >= 60) currentTime -= 16f * Time.deltaTime;
        else if (playerController.count >= 50) currentTime -= 17f * Time.deltaTime;
        else if (playerController.count >= 40) currentTime -= 18f * Time.deltaTime;
        else if (playerController.count >= 30) currentTime -= 19f * Time.deltaTime;
        else if (playerController.count >= 20) currentTime -= 20f * Time.deltaTime;
        else if (playerController.count >= 10) currentTime -= 21f * Time.deltaTime;
        else if (playerController.count >= 0)  currentTime -= 21f * Time.deltaTime;

        if (currentTime <= 0) {
            SceneManager.LoadScene(nextSceneName);
            Debug.Log("GameOver - Timeover");
        }
    }

    public void ReduceTime(float timeDamage) {
        currentTime -= timeDamage;
    }

    public void IncreaseTime(float timeIncrease) {
        currentTime += timeIncrease;

        if (currentTime > maxTime) {
            currentTime = maxTime;
        }
    }
}
