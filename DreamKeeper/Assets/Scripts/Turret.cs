using System;
using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    AudioSource shotAudioSource;

    private void Update()
    {
        Timer += Time.deltaTime;

        if(Timer >= CoolDown)
        {
            if(GameState.s_Nightmares.Count > 0)
            {
                var closestNightmare = GameState.s_Nightmares[0];
                Vector2 a = transform.position;
                Vector2 b = GameState.s_Nightmares[0].GetComponentInChildren<NightmareMover>().transform.position;
                var distance = Vector2.Distance(a, b);

                for(int i = 1; i < GameState.s_Nightmares.Count; i++)
                {
                    b = GameState.s_Nightmares[i].GetComponentInChildren<NightmareMover>().transform.position;
                    var d = Vector2.Distance(a, b);
                    if(d < distance)
                    {
                        distance = d;
                        closestNightmare = GameState.s_Nightmares[i];
                    }
                }

                var direction3D = 
                    closestNightmare.GetComponentInChildren<NightmareMover>().transform.position - transform.position;
                var direction2D = new Vector2(direction3D.x, direction3D.y);
                direction2D.Normalize();
            
                var projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
                projectile.Direction = direction2D;
                shotAudioSource.pitch = UnityEngine.Random.Range(1.2f, 1.4f);
                shotAudioSource.Play(0);
            }

            Timer = 0f;
        }
        
        foreach(var nightmare in GameState.s_Nightmares)
        {
            if(Vector2.Distance(transform.position, nightmare.transform.position) 
               < DamageDistance)
            {
                Health -= DamageTaken;
                if(Health <= 0)
                {
                    Destroy(gameObject);
                    GameState.s_Turrets.Remove(this);
                }
            }
        }
    }

    private void Awake()
    {
        GameState.s_Turrets.Add(this);
        shotAudioSource = ShotSoundSource.GetComponent<AudioSource>();
    }

    public float CoolDown = 1f;
    
    public float DamageDistance = 2f;
    public int DamageTaken = 5;
    
    public float Timer = 0f;
    public int Health = 50;
    public Projectile Projectile;
    
    public GameObject ShotSoundSource;
}