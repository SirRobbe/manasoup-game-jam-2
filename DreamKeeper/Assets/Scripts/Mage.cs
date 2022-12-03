using UnityEngine;

public class Mage : MonoBehaviour
{
    private void Update()
    {
        Timer += Time.deltaTime;
        
        if(Input.GetKeyDown(KeyCode.Alpha1) && Timer > Cooldown)
        {
            Player.IsNextProjectileFireball = true;
            Timer = 0f;
        }
    }

    private void Awake()
    {
        Timer = Cooldown;
    }

    public float Cooldown = 5f;
    [HideInInspector] public float Timer = 0f;
    public Player Player;
}
