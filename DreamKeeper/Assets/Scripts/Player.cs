using System;
using UnityEngine;

public class Player : MonoBehaviour
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
            var prefab = IsNextProjectileFireball ? Fireball : Projectile;
            IsNextProjectileFireball = false;
            var projectile = Instantiate(prefab, transform.position, Quaternion.identity);
            var worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 dir = worldPosition - transform.position;
            dir.Normalize();
            projectile.Direction = dir;
            Timer = 0f;
        }
    }

    public float Speed = 5f;
    public float Cooldown = 0.15f;
    [HideInInspector] public float Timer = 0f;
    [HideInInspector] public bool IsNextProjectileFireball = false;
    
    public Projectile Projectile;
    public Projectile Fireball;
}