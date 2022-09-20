using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2ClearFlag : MonoBehaviour
{
    private PlayerController playerController;
    private TimeLimit timeLimit;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        timeLimit = GetComponent<TimeLimit>();
    }

    void Update()
    {
        GameManager.Instance.LoadStage(
                () => { return timeLimit.CurrentTime <= 0; },
                GameManager.Instance.CutScene5);

        GameManager.Instance.LoadStage(
                () => { return playerController.count <= 0; },
                GameManager.Instance.CutScene6,
                true ,1.0f);
    }
}
