using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> MonsterList;
    [SerializeField]
    private Transform[] spawner;

    float SpawnTime;
    void Start()
    {
        SpawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        Spawn();
    }

    void Spawn()
    {
        if(Time.time - SpawnTime > 6f)
        {
            int randomMonster, randomSpawner;
            randomMonster = Random.Range(0, MonsterList.Count);
            randomSpawner = Random.Range(0, spawner.Length);
            SpawnTime = Time.time;
            Instantiate(MonsterList[randomMonster], spawner[randomSpawner].position, Quaternion.identity);
        }
    }
}
