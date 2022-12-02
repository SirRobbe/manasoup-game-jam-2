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
        
        var movement = new Vector3(Direction.x, Direction.y) * (Time.deltaTime * Speed);
        transform.position += movement;
    }

    public float LifeTime = 1f;
    public float Speed = 20f;
    [HideInInspector] public Vector2 Direction;
}