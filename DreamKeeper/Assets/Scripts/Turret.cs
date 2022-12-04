using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Turret : MonoBehaviour
{
    AudioSource shotAudioSource;

    private void Update()
    {
        ShootTimer += Time.deltaTime;

        if(ShootTimer >= ShotCooldown)
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

            ShootTimer = 0f;
        }

        DamageTimer += Time.deltaTime;

        if(DamageTimer >= DamageCooldown)
        {
            foreach(var nightmare in GameState.s_Nightmares)
            {
                var distance = Vector3.Distance(transform.position, nightmare.GetComponentInChildren<NightmareMover>().transform.position);
                if(distance < DamageDistance)
                {
                    Health -= DamageTaken;
                    if(Health <= 0)
                    {
                        Destroy(gameObject);
                        GameState.s_Turrets.Remove(this);
                    }
                }
            }
            
            DamageTimer = 0f;
        }
    }

    private void Awake()
    {
        GameState.s_Turrets.Add(this);
        shotAudioSource = ShotSoundSource.GetComponent<AudioSource>();
    }

    [FormerlySerializedAs("CoolDown")] public float ShotCooldown = 1f;
    public float DamageCooldown = 0.5f;
    
    public float DamageDistance = 2f;
    public int DamageTaken = 5;
    
    [HideInInspector] [FormerlySerializedAs("Timer")] public float ShootTimer = 0f;
    [HideInInspector] public float DamageTimer = 0f; 
        
    public int Health = 50;
    public Projectile Projectile;
    
    public GameObject ShotSoundSource;
}