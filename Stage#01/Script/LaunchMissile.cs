using UnityEngine;

public class LaunchMissile : MonoBehaviour {
    [SerializeField]
    private GameObject Missile;

    public void Luanch() {
        Instantiate(Missile, new Vector3(-4.06f, 0.23f, 0.0f), Quaternion.identity);
    }
}
