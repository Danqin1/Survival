using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Zombie;
    [SerializeField] private int mapWidth = 100;
    [SerializeField] private float spawnDelay = 5;

    void Start()
    {
        StartCoroutine(SpawnZombie());
        transform.position = new Vector3(mapWidth / 2, 0, mapWidth / 2);
    }

    IEnumerator SpawnZombie()
    {
        Vector3 newDestination = Random.insideUnitSphere * mapWidth / 2;
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
