using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiater : MonoBehaviour
{
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= SpawnTimer)
        {
            var position = new Vector3(Random.Range(-10.0f, 10.0f),Random.Range(0f, 5f), 0 );
            var nightmare = Instantiate(Nightmare, position, Quaternion.identity);
            Timer = 0f;
        }
    }

    public float SpawnTimer = 2f;
    public float Timer = 0f;
    public GameObject Nightmare;
}
