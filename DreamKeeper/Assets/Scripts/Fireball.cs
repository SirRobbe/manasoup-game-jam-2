using UnityEngine;

public class Fireball : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag(Projectile.TargetTag))
        {
            var dod = col.gameObject.AddComponent<DamageOverTime>();
            dod.Duration = Duration;
            dod.Damage = Damage;
        }
    }

    private void Awake()
    {
        Projectile = GetComponent<Projectile>();
    }

    public float Duration = 2f;
    public int Damage = 4;
    [HideInInspector] public Projectile Projectile;
}