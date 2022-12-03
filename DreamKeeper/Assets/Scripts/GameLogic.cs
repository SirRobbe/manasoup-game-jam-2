using UnityEngine;

public class GameLogic : MonoBehaviour
{
    public void Begin()
    {
        Hero.enabled = true;
        EnemySpawner.enabled = true;
        CardSlotOne.Activate(GameState.s_SelectedCardTypes[0]);
        CardSlotTwo.Activate(GameState.s_SelectedCardTypes[1]);
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
    public CardSlot CardSlotOne;
    public CardSlot CardSlotTwo;
}