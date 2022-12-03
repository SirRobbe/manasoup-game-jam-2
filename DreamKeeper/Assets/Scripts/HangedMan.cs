using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangedMan : MonoBehaviour
{
    private void Start()
    {
        Nightmares = EnemyInstantiater.s_Nightmares;
    }
    private void Update()
    {
        Timer += Time.deltaTime;
    }

    public void Do()
    {
        if(Timer >= CoolDown)
        {
            return;
        }
        
        Timer = CoolDown;
        
        StartCoroutine(ActivateEffect());
    }
    
    IEnumerator ActivateEffect()
    {
        for(int i = 0; i< Nightmares.Count; i++)
        {
            Nightmares[i].CurrentState = Nightmare.State.Flee;

            yield return new WaitForSeconds(Duration);

            Nightmares[i].CurrentState = Nightmare.State.AttackHero;
        }
    }
    public List<Nightmare> Nightmares;
    public float CoolDown = 15f;
    public float Duration = 5f;
    [HideInInspector] public float Timer = 0f;
}
