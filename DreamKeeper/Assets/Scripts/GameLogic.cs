using UnityEngine;

public class GameLogic : MonoBehaviour
{
    private void Awake()
    {
        Player.gameObject.SetActive(false);
        Healthbar.gameObject.SetActive(false);
    }
    
    public void Begin()
    {
        Hero.enabled = true;
        EnemySpawner.enabled = true;
        Player.gameObject.SetActive(true);
        Healthbar.gameObject.SetActive(true);
        CardSlotOne.Activate(GameState.s_SelectedCardTypes[0]);
        CardSlotTwo.Activate(GameState.s_SelectedCardTypes[1]);
        
        MainMenuMusic.Stop();
        BackgroundMusic.Play();
    }

    public void End()
    {
        Hero.enabled = false;
        EnemySpawner.enabled = false;
        Player.gameObject.SetActive(false);
        Healthbar.gameObject.SetActive(false);
        CardSlotOne.Deactivate();
        CardSlotTwo.Deactivate();
        
        foreach (var nightmare in GameState.s_Nightmares)
        {
            nightmare.Damage(nightmare.Health);
        }
        GameState.s_Nightmares.Clear();
        
        foreach (var turret in GameState.s_Turrets)
        {
            Destroy(turret.gameObject);
        }
        GameState.s_Turrets.Clear();
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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            CardSlotOne.OnClick();
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            CardSlotTwo.OnClick();
        }
    }

    public Hero Hero;
    public Player Player;
    public EnemyInstantiater EnemySpawner;
    public CardSlot CardSlotOne;
    public CardSlot CardSlotTwo;
    public Healthbar Healthbar;
    public AudioSource BackgroundMusic;
    public AudioSource MainMenuMusic;
}