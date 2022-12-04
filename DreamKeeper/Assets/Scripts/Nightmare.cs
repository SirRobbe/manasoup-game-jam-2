using System.Collections;
using UnityEngine;

public class Nightmare : MonoBehaviour
{
    private void Awake()
    {
        GameState.s_Nightmares.Add(this);
    }

    public bool Damage(int damage, bool muteAudio = false)
    {
        if(Health < 0f)
        {
            return Health <= 0;
        }

        if(!muteAudio)
        {
            DamageSound.Play();    
        }

        Health -= damage;
        if(Health <= 0)
        {
            StartCoroutine(Kill(muteAudio));
        }

        return Health <= 0;
    }

    private IEnumerator Kill(bool muteAudio)
    {
        Mover.IsDead = true;
        Mover.CurrentState = NightmareMover.State.BounceBack;
        Collider.enabled = false;
        
        DamageSound.Stop();
        if(!muteAudio)
        {
            DeathSound.Play();    
        }
        
        float alphaDecreasePerSecond = 1f / DeathSound.clip.length;
        float alpha = 1f;
        
        while(alpha > 0f)
        {
            alpha -= alphaDecreasePerSecond * Time.deltaTime;
            var color = Base.color;
            color.a = alpha;
            Base.color = color;
            
            color = Cone.color;
            color.a = alpha;
            Cone.color = color;
            
            yield return null;    
        }
        
        Destroy(gameObject);
    }
    
    public int Health = 100;
    public NightmareMover Mover;
    public AudioSource DeathSound;
    public AudioSource DamageSound;
    public SpriteRenderer Base;
    public SpriteRenderer Cone;
    public Collider2D Collider;
}