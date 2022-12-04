using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke : ACardManager
{
    AudioSource audioSource;

    private void Start()
    {
        Nightmares = GameState.s_Nightmares;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Timer -= Time.deltaTime;
        Timer = Mathf.Clamp(Timer, 0f, CoolDown);
    }
    IEnumerator ActivateEffect()
    {
        audioSource.Play(0);
        foreach (var nightmare in Nightmares)
        {
            nightmare.transform.Find("NightmareBody/ParticleSystemDeath").GetComponent<ParticleSystem>().Play();
        }
        yield return new WaitForSeconds(1f);

        Nightmares[0].Damage(Nightmares[0].Health); 
        for(int i = 1; i< Nightmares.Count; i++)
        {
            Nightmares[i].Damage(Nightmares[i].Health, true); 
        }
        
        Nightmares.Clear();
    }
    public void NukeNightmares()
    {
        if(Timer > 0)
        {
            return;
        }
        Timer = CoolDown;

        StartCoroutine(ActivateEffect());
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
