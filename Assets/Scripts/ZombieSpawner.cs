using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject Zombie;
    public int mapWidth = 100;
    private float spawnDelay = 5;
    void Start()
    {
        StartCoroutine(SpawnZombie());
        transform.position = new Vector3(mapWidth / 2, 0, mapWidth / 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SpawnZombie()
    {
        Vector3 newDestination = Random.insideUnitSphere * mapWidth/2;
        newDestination += transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(newDestination, out hit, mapWidth / 2, 1))
        {
            Instantiate(Zombie, hit.position, Quaternion.identity);
        }
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(SpawnZombie());
    }
}
