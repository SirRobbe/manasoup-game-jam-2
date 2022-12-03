using UnityEngine;

public class Nightmare : MonoBehaviour
{
    private void Awake()
    {
        GameState.s_Nightmares.Add(this);
    }

    public bool Damage(int damage)
    {
        Health -= damage;
        if(Health <= 0)
        {
            Destroy(gameObject);
        }

        return Health <= 0;
    }

    public int Health = 100;

}