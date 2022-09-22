using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeLimit : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private float  timeDamage   = 5.0f;
    [SerializeField]
    private float  timeIncrease = 3.0f;
    [SerializeField]
    private string nextSceneName;
    
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

        if (currentTime <= 0) {
        SceneManager.LoadScene(nextSceneName);
        }
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
