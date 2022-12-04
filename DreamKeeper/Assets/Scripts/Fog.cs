using UnityEngine;

public class Fog : MonoBehaviour
{
    private void Awake()
    {
        Hero = FindObjectOfType<Hero>();
    }

    void Start()
    {
        if(!Hero)
        {
            return;
        }
        
        TargetPosition = Hero.transform.position;
    }
    
    void Update()
    {
        var toHero = transform.position - TargetPosition;
        float angle = Vector2.Angle(Vector2.right, toHero);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = Nightmare.transform.position;
    }

    public GameObject Nightmare;
    [HideInInspector] public Hero Hero;
    public Vector3 TargetPosition;
}
