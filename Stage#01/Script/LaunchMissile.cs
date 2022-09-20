using System.Collections;
using UnityEngine;

public class LaunchMissile : MonoBehaviour
{
    [SerializeField]
    private GameObject Sky;
    [SerializeField]
    private GameObject Desk;
    [SerializeField]
    private GameObject Hand;
    [SerializeField]
    private GameObject Button;
    [SerializeField]
    private GameObject Missile_1;
    [SerializeField]
    private GameObject Missile_2;
    [SerializeField]
    private GameObject Missile_3;

    private SpriteRenderer deskSpriteRenderer;
    private SpriteRenderer buttonSpriteRenderer;
    private float          rgb;
    private bool           first = true;

    public void Awake() {
        deskSpriteRenderer = Desk.GetComponent<SpriteRenderer>();
        buttonSpriteRenderer = Button.GetComponent<SpriteRenderer>();
    }

    public void Launch() {
        if (first) {
            StartCoroutine("PushButton");
            StartCoroutine("BackgroundDim");

            var Object = Instantiate(Missile_1, new Vector3(0f, -5.0f, 0f), Quaternion.identity);
                         Instantiate(Missile_2, new Vector3(0f, -5.0f, 0f), Quaternion.identity);
                         Instantiate(Missile_3, new Vector3(0f, -5.0f, 0f), Quaternion.identity);

            GameManager.Instance.LoadStage(
                () => { return Object != null; },
                GameManager.Instance.CutScene2,
                false ,2.0f
            );

            first = false;
        }
    }

    private IEnumerator PushButton() {
        Hand.SetActive(true);
        Button.SetActive(false);
        yield return new WaitForSeconds(0.3f);
        Hand.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        Button.SetActive(true);
    }

    private IEnumerator BackgroundDim() {
        Color deskColor = deskSpriteRenderer.color;
        Color buttonColor = buttonSpriteRenderer.color;
        rgb = 255f;
        while (rgb > 60f) {
            yield return new WaitForSeconds(0.1f);
            deskColor   = new Color (rgb/255f, rgb/255f, rgb/255f, 1f);
            buttonColor = new Color (rgb/255f, rgb/255f, rgb/255f, 1f);
            deskSpriteRenderer.color   = deskColor;
            buttonSpriteRenderer.color = buttonColor;
            rgb -= 5f;
        }

        StartCoroutine("BackgroundBrightening");
    }

    private IEnumerator BackgroundBrightening() {
        Color deskColor = deskSpriteRenderer.color;
        Color buttonColor = buttonSpriteRenderer.color;

        while (rgb < 255f) {
            yield return new WaitForSeconds(0.1f);
            deskColor   = new Color (rgb/255f, rgb/255f, rgb/255f, 1f);
            buttonColor = new Color (rgb/255f, rgb/255f, rgb/255f, 1f);
            deskSpriteRenderer.color   = deskColor;
            buttonSpriteRenderer.color = buttonColor;
            rgb += 12f;
        }
    }
}