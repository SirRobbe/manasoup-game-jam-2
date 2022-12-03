using UnityEngine;

public class DamageOverTime : MonoBehaviour
{
    private void Awake()
    {
        Target = GetComponentInParent<Nightmare>();
    }

    private void Update()
    {
        SecondTimer += Time.deltaTime;
        if(SecondTimer >= 1f)
        {
            var targetKilled = Target.Damage(Damage);
            if(targetKilled)
            {
                GameState.s_Nightmares.Remove(Target);
            }
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