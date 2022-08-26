using UnityEngine;
using System.Collections;

public class AnimationDestroyer : MonoBehaviour {
    [SerializeField]
    private float delay = 1.0f;

    void Start () {
        Destroy (gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
    }
}
