using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiater : MonoBehaviour
{
    void Update()
    {
        SpawnTimer -= Time.deltaTime;
        if (SpawnTimer < 0)
        {
            var position = new Vector3(Random.Range(-10.0f, 10.0f),Random.Range(0f, 5f), 0 );
            Instantiate(Nightmare, position, Quaternion.identity);
            SpawnTimer = 1f;
        }
    }
    
    public float SpawnTimer = 2f;
    public GameObject Nightmare;
}
