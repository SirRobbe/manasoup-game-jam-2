using System;
using UnityEngine;

public class Turret : MonoBehaviour
{
    private void Update()
    {
        Timer += Time.deltaTime;

        if(Timer >= CoolDown)
        {
            if(GameState.s_Nightmares.Count > 0)
            {
                var closestNightmare = GameState.s_Nightmares[0];
                var distance = Vector2.Distance(transform.position, GameState.s_Nightmares[0].transform.position);

                for(int i = 1; i < GameState.s_Nightmares.Count; i++)
                {
                    var d = Vector2.Distance(transform.position,
                                                 GameState.s_Nightmares[i].transform.position);
                    if(d < distance)
                    {
                        distance = d;
                        closestNightmare = GameState.s_Nightmares[i];
                    }
                }

                var direction3D = closestNightmare.transform.position - transform.position;
                var direction2D = new Vector2(direction3D.x, direction3D.y);
                direction2D.Normalize();
            
                var projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
                projectile.Direction = direction2D;
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
    }

    public float CoolDown = 1f;
    
    public float DamageDistance = 2f;
    public int DamageTaken = 5;
    
    public float Timer = 0f;
    public int Health = 50;
    public Projectile Projectile;
}