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
        CooldownTimer = Mathf.Clamp(CooldownTimer, 0f, Cooldown);
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
        SpawnPosition = new Vector3(Hero.transform.position.x -0.35f, Hero.transform.position.y, Hero.transform.position.z);
        BarrierInstance = Instantiate(BarrierPrefab, SpawnPosition, Quaternion.identity);
    }

    public override float GetCooldown()
    {
        return CooldownTimer;
    }

    private Vector3 SpawnPosition;
    public Barrier BarrierPrefab;
    public Hero Hero;
    public float Cooldown = 10f;
    public float Duration = 5f;
    
    [HideInInspector] public float CooldownTimer = 0;
    [HideInInspector] public float DurationTimer = 0;
    [HideInInspector] public Barrier BarrierInstance;
}
