using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private void Update()
    {
        Timer += Time.deltaTime;
        
        int horizontal = Convert.ToInt32(Input.GetKey(KeyCode.D)) - Convert.ToInt32(Input.GetKey(KeyCode.A));
        int vertical = Convert.ToInt32(Input.GetKey(KeyCode.W)) - Convert.ToInt32(Input.GetKey(KeyCode.S));

        var movement = new Vector3(horizontal, vertical, 0);
        movement.Normalize();

        transform.position += movement * (Time.deltaTime * Speed);

        if(Input.GetMouseButton(0) && Timer > Cooldown)
        {
            var projectile = Instantiate(Projectile, transform.position, Quaternion.identity);
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            projectile.Direction = (worldPosition - transform.position).normalized;
            Timer = 0f;
        }
    }

    public float Speed = 5f;
    public float Cooldown = 0.15f;
    [HideInInspector] public float Timer = 0f;
    
    public Projectile Projectile;
}