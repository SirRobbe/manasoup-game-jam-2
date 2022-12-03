using System;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : MonoBehaviour
{
    private void Start()
    {
        Nightmares = EnemyInstantiater.s_Nightmares;
    }

    private void Update()
    {
        Timer += Time.deltaTime;
    }

    public void NukeNightmares()
    {
        if(Timer >= CoolDown)
        {
            return;
        }
        
        Timer = CoolDown;
        
        for (int i = 0; i< Nightmares.Count; i++)
        {
            Nightmares[i].Damage(Nightmares[i].Health);
        }
    }

    public List<Nightmare> Nightmares;
    public float CoolDown = 60f;
    [HideInInspector] public float Timer = 0f;
}
