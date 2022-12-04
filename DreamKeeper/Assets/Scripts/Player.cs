using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    AudioSource shotAudioSource;
    private void Awake()
    {
        Cam = Camera.main;
        CamTarget = transform.position;
        shotAudioSource = ShotSoundSource.GetComponent<AudioSource>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        
        int horizontal = Convert.ToInt32(Input.GetKey(KeyCode.D)) - Convert.ToInt32(Input.GetKey(KeyCode.A));
        int vertical = Convert.ToInt32(Input.GetKey(KeyCode.W)) - Convert.ToInt32(Input.GetKey(KeyCode.S));

        var movement = new Vector3(horizontal, vertical, 0);
        movement.Normalize();

        float maxX = 14f;
        float maxY = 7f;
        
        var newPlayPos = transform.position + movement * (Time.deltaTime * Speed);
        newPlayPos.x = Mathf.Clamp(newPlayPos.x, -maxX, maxX);
        newPlayPos.y = Mathf.Clamp(newPlayPos.y, -maxY, maxY);
        transform.position = newPlayPos;

        Renderer.flipX = horizontal == 1 ? true : false;
        
        if(newPlayPos.x <= -maxX || newPlayPos.x >= maxX)
        {
            movement.x = 0f;
        }
        
        if(newPlayPos.y <= -maxY || newPlayPos.y >= maxY)
        {
            movement.y = 0f;
        }
        
        CamTarget += (movement * (Time.deltaTime * Speed * 0.2f));
        var newPos = Vector3.Lerp(Cam.transform.position, CamTarget, Time.deltaTime * 10);
        newPos.x = Mathf.Clamp(newPos.x, -1.4f, 1.4f);
        newPos.y = Mathf.Clamp(newPos.y, 0f, 0.7f);
        newPos.z = -10f;
        Cam.transform.position = newPos;
        
        
        if(Input.GetMouseButton(0) && Timer > Cooldown)
        {
            var prefab = IsNextProjectileFireball ? Fireball : Projectile;
            IsNextProjectileFireball = false;
            var projectile = Instantiate(prefab, transform.position, Quaternion.identity);
            shotAudioSource.Play(0);
            var screenPostDepth = Input.mousePosition;
            screenPostDepth.z = transform.position.z - Cam.transform.position.z;
            var worldPosition = Camera.main.ScreenToWorldPoint(screenPostDepth);
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
    [HideInInspector] public Camera Cam;
    [HideInInspector] public Vector3 CamTarget;
    public GameObject ShotSoundSource;
    [HideInInspector] public SpriteRenderer Renderer;
    
    public Projectile Projectile;
    public Projectile Fireball;
}