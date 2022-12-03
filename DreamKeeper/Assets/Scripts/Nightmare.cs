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
    private void Start()
    {
        Hero = GameObject.FindGameObjectWithTag("Hero");
        TargetPosition = Hero.transform.position;
        SmoothTime = Random.Range(SmoothTimeRange.x, SmoothTimeRange.y);
        MaxVelocity = Random.Range(MaxVelocityRange.x, MaxVelocityRange.y);
        SinusWaveStartValue = Random.value;
    }

    private void Update()
    {
        if(Hero == null)
        {
            return;
        }
        CurrentPosition = transform.position;
        DistanceToTarget = Vector2.Distance(CurrentPosition, TargetPosition);
        SinusWaveTime = (SinusWaveStartValue + Time.time) % 360;
        SinWave = new Vector3(Mathf.Sin(SinusWaveTime), Mathf.Sin(SinusWaveTime), 0f) * DistanceToTarget;
        transform.position = Vector2.SmoothDamp(CurrentPosition, 
            TargetPosition + SinWave, ref Velocity, SmoothTime, MaxVelocity);
        Vector2 targetPosition = Hero.transform.position;
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, 
            smoothTime, maxVelocity);
    }

    public void Damage(int damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int Health = 100;
    public Vector2 smoothTimeRange;
    public Vector2 maxVelocityRange;
    private float smoothTime;
    private float maxVelocity;
    private Vector2 velocity = Vector2.zero;
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
}