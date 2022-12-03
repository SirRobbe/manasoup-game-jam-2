using UnityEngine;

public class BarrierManager : ACardManager
{
    public void Awake()
    {
        Hero = FindObjectOfType<Hero>();
        if(!Hero)
        {
            Debug.LogError("Hero not found");
        }
    }
    
    public void Update()
    {
        CooldownTimer -= Time.deltaTime;
        DurationTimer -= Time.deltaTime;

        if(DurationTimer <= 0 && BarrierInstance)
        {
            Destroy(BarrierInstance.gameObject);
        }
    }

    public override void Invoke()
    {
        if(!BarrierPrefab || !Hero || CooldownTimer > 0)
        {
            return;
        }

        CooldownTimer = Cooldown;
        DurationTimer = Duration;

        BarrierInstance = Instantiate(BarrierPrefab, Hero.transform.position, Quaternion.identity);
    }

    public override float GetCooldown()
    {
        return CooldownTimer;
    }

    public Barrier BarrierPrefab;
    public Hero Hero;
    public float Cooldown = 10f;
    public float Duration = 5f;
    
    [HideInInspector] public float CooldownTimer = 0;
    [HideInInspector] public float DurationTimer = 0;
    [HideInInspector] public Barrier BarrierInstance;
}
