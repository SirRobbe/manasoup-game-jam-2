using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    void Start()
    {
        OwnPosition = this.transform.position;
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= Tick)
        {
            CheckForCloseNightmares();
            Timer = 0f;
        }
    }

    void CheckForCloseNightmares()
    {
        foreach (var i in GameState.s_Nightmares)
        {
            if (Vector2.Distance(OwnPosition, i.transform.position)<DamageDistance)
            {
                DreamDepth -= DamageTaken;
            }
        }
    }
    public float Tick = 1f;
    public float Timer = 0f;
    public int DamageTaken = 1;
    public float DamageDistance = 2f;
    public int DreamDepth = 100;
    private Vector2 OwnPosition;
}
