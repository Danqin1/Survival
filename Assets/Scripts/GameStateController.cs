using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private SurviveTimer surviveTimer;

    private void Start()
    {
        surviveTimer.onTimerEnd.AddListener(OnTimerEnd);
    }

    private void OnDestroy()
    {
        surviveTimer.onTimerEnd.RemoveListener(OnTimerEnd);
    }

    private void OnTimerEnd()
    {
        Debug.Log("Timer end");
    }
}
