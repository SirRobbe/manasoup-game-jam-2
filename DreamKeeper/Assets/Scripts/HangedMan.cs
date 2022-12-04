using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangedMan : ACardManager
{
    private void Start()
    {
        Nightmares = GameState.s_Nightmares;
    }
    private void Update()
    {
        Timer -= Time.deltaTime;
        Timer = Mathf.Clamp(Timer, 0, CoolDown);
    }

    IEnumerator ActivateEffect()
    {
        foreach (var nightmare in Nightmares)
        {
            nightmare.transform.Find("NightmareBody/ParticleSystemHangedMan").GetComponent<ParticleSystem>().Play();
            nightmare.GetComponentInChildren<NightmareMover>().CurrentState = NightmareMover.State.Flee;
        }
        yield return new WaitForSeconds(Duration);
        foreach(var nightmare in Nightmares)
        {
            nightmare.GetComponentInChildren<NightmareMover>().CurrentState = NightmareMover.State.AttackHero;
        }
    }

    public override void Invoke()
    {
        if(Timer > 0f)
        {
            return;
        }

        Timer = CoolDown;
        StartCoroutine(ActivateEffect());
    }

    public override float GetCooldown()
    {
        return Timer;
    }
    
    public float CoolDown = 15f;
    public float Duration = 5f;
    [HideInInspector] public float Timer = 0f;
    public List<Nightmare> Nightmares;

}
