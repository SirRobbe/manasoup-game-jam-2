using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    void Start()
    {
        Hero = GameObject.FindGameObjectWithTag("Hero");
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
    public GameObject Hero;
    public Vector3 TargetPosition;
}
