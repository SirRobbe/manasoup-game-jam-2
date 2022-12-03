using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiater : MonoBehaviour
{
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= SpawnTimer)
        {
            var position = new Vector3(Random.Range(-20.0f, 20.0f),Random.Range(10f, 20f), 0 );
            var nightmare = Instantiate(Nightmare, position, Quaternion.identity).GetComponentInChildren<NightmareMover>();
            nightmare.Renderer.sprite = NightmareSprites[Random.Range(0, NightmareSprites.Count)];
            Timer = 0f;
        }
    }

    public float SpawnTimer = 2f;
    public float Timer = 0f;
    public GameObject Nightmare;

    public List<Sprite> NightmareSprites;
}
