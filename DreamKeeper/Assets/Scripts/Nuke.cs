using UnityEngine;

public class Nuke : MonoBehaviour
{
    private void Update()
    {
        Timer -= Time.deltaTime;
    }

    public void Invoke()
    {
        if(Timer > 0)
        {
            return;
        }
        
        Timer = Cooldown;
        
        foreach (var nightmare in EnemyInstantiater.s_Nightmares)
        {
            Destroy(nightmare.gameObject);
        }
    }

    public float Cooldown = 60f;
    [HideInInspector] public float Timer = 0f;
}
