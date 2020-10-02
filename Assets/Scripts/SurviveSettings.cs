using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "SurviveTimeSettings", menuName = "Survival/SurviveTime Settings")]
public class SurviveSettings : ScriptableObject
{
    [SerializeField] private float timeToSurvive;
    [SerializeField] private float zombieAgroRadius;
    [SerializeField] private int zombieDamage;
    [SerializeField] private float zombieDamageRate;

    public float TimeToSurvive => timeToSurvive;
    public float ZombieAgroRadius => zombieAgroRadius;
    public int ZombieDamage => zombieDamage;
    public float ZombieDamageRate => zombieDamageRate;
}