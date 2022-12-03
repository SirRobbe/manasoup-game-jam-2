using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    void Awake()
    {
        GameLogic = FindObjectOfType<GameLogic>();
    }
    
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
        foreach (var nightmare in GameState.s_Nightmares)
        {
            var nightmarePosition = nightmare.gameObject.GetComponentInChildren<NightmareMover>().transform.position;
            if (Vector2.Distance(OwnPosition, nightmarePosition) < DamageDistance)
            {
                DreamDepth -= DamageTaken;
            }

            if(DreamDepth <= 0)
            {
                GameLogic.End();
            }
        }
    }
    
    public float Tick = 1f;
    public float Timer = 0f;
    public int DamageTaken = 1;
    public float DamageDistance = 2f;
    public int DreamDepth = 100;
    private Vector2 OwnPosition;
    private GameLogic GameLogic;
}
