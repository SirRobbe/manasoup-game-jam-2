using UnityEngine;

public class Lovers : MonoBehaviour
{
    private void Update()
    {
        Timer += Time.deltaTime;
        
        if(Input.GetKeyDown(KeyCode.Alpha1) && Timer > Cooldown)
        {
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
    }

    private void Awake()
    {
        Timer = Cooldown;
    }

    public float Duration = 2f;
    public int Damage = 4;
    public float Range = 1f;
    public float Cooldown = 5f;
    [HideInInspector] public float Timer = 0f;
    public Player Player;
}
