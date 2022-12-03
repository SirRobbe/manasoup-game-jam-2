using UnityEngine;

public class Lovers : ACardManager
{
    private void Update()
    {
        Timer -= Time.deltaTime;
        Timer = Mathf.Clamp(Timer, 0, Cooldown);
    }

    public override void Invoke()
    {
        if(Timer > 0f)
        {
            return;
        }
        
        foreach(var nightmare in GameState.s_Nightmares)
        {
            float distance = Vector2.Distance(nightmare.transform.position, Player.transform.position);
            if(distance <= Range)
            {
                var dod = nightmare.gameObject.AddComponent<DamageOverTime>();
                dod.Duration = Duration;
                dod.Damage = Damage;
            }
        }
        
        Player.IsNextProjectileFireball = true;
        Timer = 0f;
    }

    public override float GetCooldown()
    {
        return Timer;
    }
    
    private void Awake()
    {
        Timer = 0f;
        Player = GameObject.FindObjectOfType<Player>();
    }

    public float Duration = 2f;
    public int Damage = 4;
    public float Range = 1f;
    public float Cooldown = 5f;
    [HideInInspector] public float Timer = 0f;
    public Player Player;
    
}
