using UnityEngine;
using TMPro;

public class RemainText : MonoBehaviour
{
    public Player player;
    private TextMeshProUGUI textRemains;

    private void Awake() {
        textRemains = GetComponent<TextMeshProUGUI>();
    }

    private void Update() {
        textRemains.text = "X " + player.Count;
    }
}
