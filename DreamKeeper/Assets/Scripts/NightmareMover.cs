using UnityEditor;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class NightmareMover : MonoBehaviour
{
    private void Awake()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Start()
    {
        SmoothTime = Random.Range(SmoothTimeRange.x, SmoothTimeRange.y);
        MaxVelocity = Random.Range(MaxVelocityRange.x, MaxVelocityRange.y);
        SinusWaveStartValue = Random.value;
        
        Target = GameObject.FindGameObjectWithTag("Hero");
        if(!Target)
        {
            return;
        }
        
        TargetPosition = Target.transform.position;
    }
    private void Update()
    {
        if(!Target)
        {
            Target = GameObject.FindGameObjectWithTag("Hero");
            TargetPosition = Target.transform.position;
        }
        UpdateOrientation();

        switch(CurrentState)
        {
            case State.AttackHero:
            {
                foreach (var turret in GameState.s_Turrets)
                {
                    float d = Vector3.Distance(transform.position, turret.transform.position);
                    if (d < TurretTriggerDistance && Random.Range(0, 1) < 0.5f)
                    {
                        Target = turret.gameObject;
                        TargetPosition = Target.transform.position;
                        CurrentState = State.AttackTurret;
                        break;
                    }
                }

                MoveAtTarget();
                break;
            }

            case State.AttackTurret:
            {
                if (Target == null)
                {
                    Target = GameObject.FindGameObjectWithTag("Hero");
                    TargetPosition = Target.transform.position;
                    CurrentState = State.AttackHero;
                }

                MoveAtTarget();
                break;
            }
            case State.BounceBack:
            {
                if (!Target)
                {
                    return;
                }

                var hero = (Vector2)Target.transform.position;
                var nightmare = (Vector2)transform.position;
                var heroToTarget = (nightmare - hero).normalized;
                float speed = IsDead ? 0.2f * MaxVelocity : MaxVelocity;
                transform.position += new Vector3(heroToTarget.x, heroToTarget.y, 0f) * (Time.deltaTime * speed);
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
        var toNightmare = transform.position - Target.transform.position;
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
        if(!Target)
        {
            return;
        }
        
        CurrentPosition = transform.position;
        DistanceToTarget = Vector2.Distance(CurrentPosition, TargetPosition);
        SinusWaveTime = (SinusWaveStartValue + Time.time);
        SinWave = new Vector2(Mathf.Sin(SinusWaveTime), Mathf.Cos(SinusWaveTime)) * DistanceToTarget;
        var newPosition = Vector2.SmoothDamp(CurrentPosition, 
            TargetPosition + SinWave, ref Velocity, SmoothTime, MaxVelocity);
        transform.position = new Vector3(newPosition.x, newPosition.y, Player.transform.position.z);

        if ((TargetPosition.y + Offset) > transform.position.y)
        {
            Velocity *= -1f;
        }
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
        var newPosition = Vector2.SmoothDamp(CurrentPosition, 
            CurrentPosition + FleeTarget, ref Velocity, SmoothTime, MaxVelocity);
        transform.position = new Vector3(newPosition.x, newPosition.y, Target.transform.position.z);
    }

    public bool IsDead = false;
    private float Offset = 0.5f;
    private float SinusWaveStartValue;
    private float SinusWaveTime;
    private Vector2 CurrentPosition;
    private Vector2 SinWave;
    private float DistanceToTarget;
    public Vector2 SmoothTimeRange = new Vector2(5,7);
    public Vector2 MaxVelocityRange = new Vector2(1,1.5f);
    private float SmoothTime;
    private float MaxVelocity;
    private Vector2 Velocity = Vector2.zero;
    private Vector2 TargetPosition;
    private GameObject Target;
    private GameObject Player;

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