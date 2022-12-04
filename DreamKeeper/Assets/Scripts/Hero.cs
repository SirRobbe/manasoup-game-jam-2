using UnityEngine;

public class Hero : MonoBehaviour
{
    AudioSource audioSource;
    void Awake()
    {
        DreamDepth = MaxDreamDepth;
        GameLogic = FindObjectOfType<GameLogic>();
        GameOverManager = FindObjectOfType<GameOverManager>();
    }
    
    void Start()
    {
        OwnPosition = this.transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Timer += Time.deltaTime;
        if (DreamDepth <= 0f)
        {
            GameOverManager.gameObject.SetActive(true);
            GameLogic.End();
        }
        else if (Timer >= Tick)
        {
            CheckForCloseNightmares();
            Timer = 0f;
        }
    }

    void CheckForCloseNightmares()
    {
        foreach (var nightmare in GameState.s_Nightmares)
        {
            var nightmarePosition = nightmare.gameObject.GetComponentInChildren<NightmareMover>().transform.position;
            if (Vector2.Distance(OwnPosition, nightmarePosition) < DamageDistance)
            {
                DreamDepth -= DamageTaken;
                audioSource.Play(0);
            }
        }
    }
    
    public float Tick = 1f;
    public float Timer = 0f;
    public int DamageTaken = 1;
    public float DamageDistance = 2f;
    public int DreamDepth = 100;
    public int MaxDreamDepth = 100;
    private Vector2 OwnPosition;
    private GameLogic GameLogic;
    private GameOverManager GameOverManager;
}
