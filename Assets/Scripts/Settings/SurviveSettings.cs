using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "SurviveTimeSettings", menuName = "Survival/SurviveTime Settings")]
public class SurviveSettings : ScriptableObject
{
    [SerializeField] private float timeToSurvive;
    [SerializeField] private float zombieAgroRadius;
    [SerializeField] private int zombieDamage;
    [SerializeField] private float zombieDamageRate;
    [SerializeField] private int initialZombies;
    [SerializeField] private int zombieSpawnDistanceFromPlayer;
    [SerializeField] private float zombieSpawnDelay;
    [SerializeField] private int mapWidth = 100;

    public float TimeToSurvive => timeToSurvive;
    public float ZombieAgroRadius => zombieAgroRadius;
    public int ZombieDamage => zombieDamage;
    public float ZombieDamageRate => zombieDamageRate;
    public int InitialZombies => initialZombies;
    public int ZombieSpawnDistanceFromPlayer => zombieSpawnDistanceFromPlayer;
    public float ZombieSpawnDelay => zombieSpawnDelay;
    public int MapWidth => mapWidth;
}