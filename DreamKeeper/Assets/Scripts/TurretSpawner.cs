using UnityEngine;

public class TurretSpawner : ACardManager
{
    private void Update()
    {
        Timer -= Time.deltaTime;
        Timer = Mathf.Clamp(Timer, 0f, Cooldown);
    }

    public override void Invoke()
    {
        if(Timer <= 0f)
        {
            Instantiate(TurretPrefab, Player.transform.position, Quaternion.identity);
            Timer = Cooldown;
        }
    }

    public override float GetCooldown()
    {
        return Timer;
    }

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }
    
    public Turret TurretPrefab;
    [HideInInspector] public Player Player;

    [HideInInspector] public float Timer = 0f;
    public float Cooldown = 15f;
}