using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class NightmareMover : MonoBehaviour
{
    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        SmoothTime = Random.Range(SmoothTimeRange.x, SmoothTimeRange.y);
        MaxVelocity = Random.Range(MaxVelocityRange.x, MaxVelocityRange.y);
        SinusWaveStartValue = Random.value;
        
        Hero = GameObject.FindGameObjectWithTag("Hero");
        if(!Hero)
        {
            return;
        }
        
        TargetPosition = Hero.transform.position;
    }
    private void Update()
    {
        UpdateOrientation();

        switch (CurrentState)
        {
            case State.AttackHero:
            {
                foreach (var turret in GameState.s_Turrets)
                {
                    float d = Vector2.Distance(transform.position, turret.transform.position);
                    if (d < TurretTriggerDistance && Random.Range(0, 1) < 0.5f)
                    {
                        Hero = turret.gameObject;
                        CurrentState = State.AttackTurret;
                        break;
                    }
                }

                MoveAtTarget();
                break;
            }

            case State.AttackTurret:
            {
                if (Hero == null)
                {
                    //Renderer.color = Color.red;
                    Hero = GameObject.FindGameObjectWithTag("Hero");
                    CurrentState = State.AttackHero;
                }

                MoveAtTarget();
                break;
            }
            case State.BounceBack:
            {
                if (!Hero)
                {
                    return;
                }

                var heroToTarget = (transform.position - Hero.transform.position).normalized;
                transform.position += heroToTarget * (Time.deltaTime * MaxVelocity);
                break;
            }
            case State.Flee:
            {
                Flee();
                break;
            }
        }
    }
    private void UpdateOrientation()
    {
        Renderer.flipX = transform.position.x < 0;
        var toNightmare = transform.position - Hero.transform.position;
        float angle = Vector2.Angle(Vector2.right, toNightmare);
        if(Renderer.flipX)
        {
            angle += 180f;
        }

        if(CurrentState == State.Flee)
        {
            angle += 180;
            Renderer.flipY = true;
        }
        
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    
    private void MoveAtTarget()
    {
        if(!Hero)
        {
            return;
        }
        CurrentPosition = transform.position;
        DistanceToTarget = Vector2.Distance(CurrentPosition, TargetPosition);
        SinusWaveTime = (SinusWaveStartValue + Time.time) % 360;
        SinWave = new Vector2(Mathf.Sin(SinusWaveTime), Mathf.Cos(SinusWaveTime)) * DistanceToTarget;
        transform.position = Vector2.SmoothDamp(CurrentPosition, 
            TargetPosition + SinWave, ref Velocity, SmoothTime, MaxVelocity);
    }

    private void AttackHero()
    {
        CurrentState = State.AttackHero;
    }

    public void BounceBack()
    {
        CurrentState = State.BounceBack;
        Invoke(nameof(AttackHero), 2);
    }
    
    private void Flee()
    {
        CurrentPosition = transform.position;
        Vector2 FleeTarget = (CurrentPosition - TargetPosition) * 10f;
        transform.position = Vector2.SmoothDamp(CurrentPosition, 
            CurrentPosition + FleeTarget, ref Velocity, SmoothTime, MaxVelocity);
    }
    
    private float SinusWaveStartValue;
    private float SinusWaveTime;
    private Vector2 CurrentPosition;
    private Vector2 SinWave;
    private float DistanceToTarget;
    public Vector2 SmoothTimeRange = new Vector2(1,10);
    public Vector2 MaxVelocityRange = new Vector2(1,10);
    private float SmoothTime;
    private float MaxVelocity;
    private Vector2 Velocity = Vector2.zero;
    private Vector2 TargetPosition;
    private GameObject Hero;

    [HideInInspector] public SpriteRenderer Renderer;
    [HideInInspector] public State CurrentState = State.AttackHero;
    
    public float TurretTriggerDistance = 0.5f;
    
    public enum State
    {
        AttackTurret,
        AttackHero,
        BounceBack,
        Flee
    }
}