using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyInstantiater : MonoBehaviour
{
    void Update()
    {
        SpawnTimer += Time.deltaTime;
        if (SpawnTimer >= InitialSpawnInterval)
        {
            var position = new Vector3(Random.Range(-20.0f, 20.0f),Random.Range(10f, 20f), Player.transform.position.z);
            var prefab = NightmarePrefabs[Random.Range(0, NightmarePrefabs.Count)];
            var mover = Instantiate(prefab, position, Quaternion.identity).GetComponentInChildren<NightmareMover>();
            mover.gameObject.transform.position = position;
            SpawnTimer = 0f;
        }

        DecreaseSpawnIntervalTimer += Time.deltaTime;
        if(DecreaseSpawnIntervalTimer >= DecreaseSpawnInterval)
        {
            DecreaseSpawnIntervalTimer = 0f;
            InitialSpawnInterval -= InitialSpawnInterval * DecreaseByPercent;
        }
    }

    public float InitialSpawnInterval = 2f;
    [HideInInspector] public float SpawnTimer = 0f;
    public GameObject Player;
    public float DecreaseSpawnInterval = 30f;
    [HideInInspector] public float DecreaseSpawnIntervalTimer = 0f;
    public float DecreaseByPercent = 0.1f;

    public List<Nightmare> NightmarePrefabs;
}
