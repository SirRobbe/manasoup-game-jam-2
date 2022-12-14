using UnityEngine;

public class Lovers : ACardManager
{
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

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
        Player.GetComponent<ParticleSystem>().Play();
        audioSource.Play(0);

        foreach(var nightmare in GameState.s_Nightmares)
        {
            float distance = Vector3.Distance(nightmare.gameObject.GetComponentInChildren<NightmareMover>().transform.position, Player.transform.position);
            if(distance <= Range)
            {
                var dod = nightmare.gameObject.AddComponent<DamageOverTime>();
                dod.Duration = Duration;
                dod.Damage = Damage;
            }
        }
        
        Timer = Cooldown;
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
