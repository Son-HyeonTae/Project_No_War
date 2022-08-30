using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Debuff : MonoBehaviour
{
    public Sprite Icon;
    public string Type;
    public float Percentage;
    public float Duration;
    public float RemainTime;

    public Entity Target;

    public virtual void Initialize(string type, float percentage, float duaration, Entity Target)
    {
        this.Type = type;
        this.Percentage = percentage;
        this.Duration = duaration;
        this.RemainTime = this.Duration;
        this.Target = Target;
    }
    protected virtual void OnEnter() { }
    protected virtual void OnUpdate() { }
    protected virtual void OnExit() { }
    public virtual void OnActive()
    {
        StartCoroutine(Activation());
    }
    protected virtual IEnumerator Activation()
    { 
        OnEnter();
        while (true)
        {
            RemainTime -= Time.deltaTime;
            OnUpdate();

            if (RemainTime <= 0)
            {
                OnExit();
                yield break;
            }
        }

    }

    private void Awake()
    {
        Icon = GetComponent<Sprite>();
    }
}
