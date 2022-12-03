using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Nightmare : MonoBehaviour
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
        
        if(!Hero)
        {
            return;
        }
        
        Hero = GameObject.FindGameObjectWithTag("Hero");
        TargetPosition = Hero.transform.position;
    }

    private void Update()
    {
        switch(CurrentState)
        {
            case State.AttackHero:
            {
                foreach(var turret in GameState.s_Turrets)
                {
                    float d = Vector2.Distance(transform.position, turret.transform.position); 
                    if(d < TurretTriggerDistance && Random.Range(0, 1) < 0.5f)
                    {
                        Hero = turret.gameObject;
                        CurrentState = State.AttackTurret;
                        Renderer.color = Color.yellow;
                        break;
                    }
                }

                MoveAtTarget();
                break;
            }

            case State.AttackTurret:
            {
                if(Hero == null)
                {
                    Renderer.color = Color.red;
                    Hero = GameObject.FindGameObjectWithTag("Hero");
                    CurrentState = State.AttackHero;
                }
                
                MoveAtTarget();
                break;
            }
        }
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
        SinWave = new Vector3(Mathf.Sin(SinusWaveTime), Mathf.Cos(SinusWaveTime), 0f) * DistanceToTarget;
        transform.position = Vector2.SmoothDamp(CurrentPosition, 
                                                TargetPosition + SinWave, ref Velocity, SmoothTime, MaxVelocity);
        Vector2 targetPosition = Hero.transform.position;
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref Velocity, 
                                                SmoothTime, MaxVelocity);
    }
    
    public void Damage(int damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            Destroy(gameObject);
            EnemyInstantiater.s_Nightmares.Remove(this);
        }
    }

    public int Health = 100;
    private float SinusWaveStartValue;
    private float SinusWaveTime;
    private Vector3 CurrentPosition;
    private Vector3 SinWave;
    private float DistanceToTarget;
    public Vector2 SmoothTimeRange = new Vector2(1,10);
    public Vector2 MaxVelocityRange = new Vector2(1,10);
    private float SmoothTime;
    private float MaxVelocity;
    private Vector2 Velocity = Vector2.zero;
    private Vector3 TargetPosition;
    private GameObject Hero;

    [HideInInspector] public SpriteRenderer Renderer;
    [HideInInspector] public State CurrentState = State.AttackHero;
    
    public float TurretTriggerDistance = 0.5f;
    
    public enum State
    {
        AttackTurret,
        AttackHero,
    }
}