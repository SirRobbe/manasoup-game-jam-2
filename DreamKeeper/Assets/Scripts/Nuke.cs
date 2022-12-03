using System;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : ACardManager
{
    private void Start()
    {
        Nightmares = GameState.s_Nightmares;
    }

    private void Update()
    {
        Timer -= Time.deltaTime;
        Timer = Mathf.Clamp(Timer, 0f, CoolDown);
    }

    public void NukeNightmares()
    {
        if(Timer > 0)
        {
            return;
        }
        
        Timer = CoolDown;
        
        for(int i = 0; i< Nightmares.Count; i++)
        {
            Nightmares[i].Damage(Nightmares[i].Health);
        }
    }

    public override void Invoke()
    {
        NukeNightmares();
    }

    public override float GetCooldown()
    {
        return Timer;
    }
    
    public List<Nightmare> Nightmares;
    public float CoolDown = 60f;
    [HideInInspector] public float Timer = 0f;
}
