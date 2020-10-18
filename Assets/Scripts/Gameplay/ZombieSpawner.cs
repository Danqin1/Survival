using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ZombieSpawner : MonoBehaviour
{
    #region variables

    [SerializeField] private GameObject Zombie;
    [SerializeField] private SurviveSettings surviveSettings;
    [SerializeField] private GameObject zombieContainer;

    #endregion

    #region Unity methods

    void Start()
    {
        StartCoroutine(SpawnZombie());
        transform.position = new Vector3(surviveSettings.MapWidth / 2, 0, surviveSettings.MapWidth / 2);

        for (int i = 0; i < surviveSettings.InitialZombies; i++)
        {
            Spawn();
        }
    }

    #endregion

    #region private methods

    private IEnumerator SpawnZombie()
    {
        Spawn();
        yield return new WaitForSeconds(surviveSettings.ZombieSpawnDelay);
        StartCoroutine(SpawnZombie());
    }

    private void Spawn()
    {
        Vector3 newDestination = Random.insideUnitSphere * surviveSettings.MapWidth / 2;
        newDestination += transform.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(newDestination, out hit, surviveSettings.MapWidth / 2, 1))
        {
            if (Vector3.Distance(hit.position, Player.instance.transform.position) > surviveSettings.ZombieSpawnDistanceFromPlayer)
            {
                GameObject z = Instantiate(Zombie, hit.position, Quaternion.identity);
                //z.GetComponent<Zombie>().surviveSettings = surviveSettings;
                z.transform.parent = zombieContainer.transform;
            }
        }
    }

    #endregion
}
