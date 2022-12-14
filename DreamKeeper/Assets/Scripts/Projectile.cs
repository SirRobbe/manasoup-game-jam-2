using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void Update()
    {
        LifeTime -= Time.deltaTime;
        if(LifeTime < 0)
        {
            Destroy(gameObject);
        }

        var movement = Direction * (Time.deltaTime * Speed);
        Rigidbody.MovePosition(Rigidbody.position + movement);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag(TargetTag))
        {
            var nightmare = collider.GetComponentInParent<Nightmare>();
            var nightmareKilled = nightmare.Damage(Damage);
            if(nightmareKilled)
            {
                GameState.s_Nightmares.Remove(nightmare);
            }
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
    }

    public string TargetTag = "";
    public int Damage = 25;
    public float LifeTime = 1f;
    public float Speed = 20f;
    [HideInInspector] public Vector2 Direction;
    [HideInInspector] public Rigidbody2D Rigidbody;
}