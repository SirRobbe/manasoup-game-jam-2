using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiater : MonoBehaviour
{
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= SpawnTimer)
        {
            var position = new Vector3(Random.Range(-20.0f, 20.0f),Random.Range(10f, 20f), Player.transform.position.z);
            var prefab = NightmarePrefabs[Random.Range(0, NightmarePrefabs.Count)];
            Instantiate(prefab, position, Quaternion.identity).GetComponentInChildren<NightmareMover>();
            Timer = 0f;
        }
    }

    public float SpawnTimer = 2f;
    public float Timer = 0f;
    public GameObject Player;

    public List<Nightmare> NightmarePrefabs;
}
