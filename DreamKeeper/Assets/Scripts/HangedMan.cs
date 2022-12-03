using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangedMan : ACardManager
{
    private void Update()
    {
        Timer -= Time.deltaTime;
        Timer = Mathf.Clamp(Timer, 0, CoolDown);
    }

    IEnumerator ActivateEffect()
    {
        for(int i = 0; i< GameState.s_Nightmares.Count; i++)
        {
            GameState.s_Nightmares[i].CurrentState = Nightmare.State.Flee;
            yield return new WaitForSeconds(Duration);
            GameState.s_Nightmares[i].CurrentState = Nightmare.State.AttackHero;
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
    
}
