using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "SurviveTimeSettings", menuName = "Survival/SurviveTime Settings")]
public class SurviveSomeTimeSettings : ScriptableObject
{
    [SerializeField] private float timeToSurvive;

    public float TimeToSurvive => timeToSurvive;
}