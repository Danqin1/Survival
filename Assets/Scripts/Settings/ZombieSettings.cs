using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "ZokmbieSettings", menuName = "Survival/Zombie Settings")]
public class ZombieSettings : ScriptableObject
{
    [SerializeField] private float timeToSurvive;
    [SerializeField] private float zombieAgroRadius;
    [SerializeField] private int zombieDamage;
    [SerializeField] private float zombieDamageRate;
    [SerializeField] private int initialZombies;
    [SerializeField] private int zombieSpawnDistanceFromPlayer;
    [SerializeField] private float zombieSpawnDelay;
    [SerializeField] private int mapWidth = 100;
    [SerializeField] private int fieldsOfViewAngle = 60;
    [SerializeField] private float stopAttackingDistance = 1.5f;
    [SerializeField] private float initialHealth = 100;
    [SerializeField] private float zombieStartAttackingDistance = 1.5f;

    public float TimeToSurvive => timeToSurvive;
    public float ZombieAgroRadius => zombieAgroRadius;
    public int ZombieDamage => zombieDamage;
    public float ZombieDamageRate => zombieDamageRate;
    public int InitialZombies => initialZombies;
    public int ZombieSpawnDistanceFromPlayer => zombieSpawnDistanceFromPlayer;
    public float ZombieSpawnDelay => zombieSpawnDelay;
    public int MapWidth => mapWidth;
    public float FieldsOfViewAngle => fieldsOfViewAngle;
    public float StopAttackingDistance => stopAttackingDistance;
    public float InitialHealth => initialHealth;
    public float ZombieStartAttackingDistance => zombieStartAttackingDistance;
}