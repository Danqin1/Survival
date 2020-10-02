using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Zombie;
    [SerializeField] private SurviveSettings surviveSettings;
    [SerializeField] private int mapWidth = 100;
    [SerializeField] private float spawnDelay = 5;
    [SerializeField] private int initialZombies = 10;

    void Start()
    {
        StartCoroutine(SpawnZombie());
        transform.position = new Vector3(mapWidth / 2, 0, mapWidth / 2);
        for (int i = 0; i < initialZombies; i++)
        {
            Spawn();
        }
    }

    IEnumerator SpawnZombie()
    {
        Spawn();
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(SpawnZombie());
    }

    private void Spawn()
    {
        Vector3 newDestination = Random.insideUnitSphere * mapWidth / 2;
        newDestination += transform.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(newDestination, out hit, mapWidth / 2, 1))
        {
            GameObject z = Instantiate(Zombie, hit.position, Quaternion.identity);
            z.GetComponent<Zombie>().surviveSettings = surviveSettings;
        }
    }
}
