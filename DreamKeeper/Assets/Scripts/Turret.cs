using UnityEngine;

public class Turret : MonoBehaviour
{
    private void Update()
    {
        Timer += Time.deltaTime;

        if(Timer >= CoolDown)
        {
            if(EnemyInstantiater.s_Nightmares.Count > 0)
            {
                var closestNightmare = EnemyInstantiater.s_Nightmares[0];
                var distance = Vector2.Distance(transform.position, EnemyInstantiater.s_Nightmares[0].transform.position);

                for(int i = 1; i < EnemyInstantiater.s_Nightmares.Count; i++)
                {
                    var d = Vector2.Distance(transform.position,
                                                 EnemyInstantiater.s_Nightmares[i].transform.position);
                    if(d < distance)
                    {
                        distance = d;
                        closestNightmare = EnemyInstantiater.s_Nightmares[i];
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
    }

    public float CoolDown = 1f;
    public float Timer = 0f;
    public Projectile Projectile;
}