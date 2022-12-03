using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public void Begin()
    {
        Hero.enabled = true;
        EnemySpawner.enabled = true;
    }
    
    private void Start()
    {
        Pause();
    }

    private void Pause()
    {
        Hero.enabled = false;
        EnemySpawner.enabled = false;
    }

    public Hero Hero;
    public EnemyInstantiater EnemySpawner;
}