using UnityEngine;

public class Missile : MonoBehaviour {
    public void Update () {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, new Vector3(4.6f, 5.45f, 0f), 0.1f);
    }
}
