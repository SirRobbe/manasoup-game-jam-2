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
        
        var nightmares = GameObject.FindGameObjectsWithTag("Nightmare");
        foreach (GameObject nightmare in nightmares)
        {
            Destroy(nightmare);
        }
    }

    public float Cooldown = 60f;
    [HideInInspector] public float Timer = 0f;
}
