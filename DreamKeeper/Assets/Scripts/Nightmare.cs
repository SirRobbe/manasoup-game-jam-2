using UnityEngine;
using Random = UnityEngine.Random;

public class Nightmare : MonoBehaviour
{
    private void Start()
    {
        Hero = GameObject.FindGameObjectWithTag("Hero");
        smoothTime = Random.Range(smoothTimeRange.x, smoothTimeRange.y);
        maxVelocity = Random.Range(maxVelocityRange.x, maxVelocityRange.y);
    }

    private void Update()
    {
        if(Hero == null)
        {
            return;
        }
        
        Vector2 targetPosition = Hero.transform.position;
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, 
            smoothTime, maxVelocity);
    }

    public void Damage(int damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public int Health = 100;
    public Vector2 smoothTimeRange;
    public Vector2 maxVelocityRange;
    private float smoothTime;
    private float maxVelocity;
    private Vector2 velocity = Vector2.zero;
    private GameObject Hero;
}