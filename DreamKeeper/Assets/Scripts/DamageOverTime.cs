using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    private void Awake()
    {
        Target = GetComponent<Nightmare>();
    }

    private void Update()
    {
        SecondTimer += Time.deltaTime;
        if(SecondTimer >= 1f)
        {
            Target.Damage(Damage);
            SecondTimer = 0f;
        }
        
        Timer += Time.deltaTime;
        if(Timer > Duration)
        {
            Destroy(this);
        }
    }

    [HideInInspector] public Nightmare Target;
    [HideInInspector] public int Damage;
    [HideInInspector] public float Duration;
    [HideInInspector] public float Timer = 0f;
    [HideInInspector] public float SecondTimer = 0f;
}