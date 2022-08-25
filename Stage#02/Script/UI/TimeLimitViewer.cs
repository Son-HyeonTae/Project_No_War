using UnityEngine;
using UnityEngine.UI;

public class TimeLimitViewer : MonoBehaviour
{
    public  TimeLimit timeLimit;
    private Slider    sliderTimeLimit;

    private void Awake() {
        sliderTimeLimit = GetComponent<Slider>();
    }

    private void Update() {
        sliderTimeLimit.value = timeLimit.CurrentTime / timeLimit.MaxTime;
    }
}
