using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SurviveTimer : MonoBehaviour
{
    #region variables

    public UnityEvent onTimerEnd;

    [SerializeField] private ZombieSettings surviveSomeTimeSettings;

    #endregion

    #region properties

    public float TimeToSurvive { get; private set; }

    #endregion

    #region Unity methods
    private void Awake()
    {
        TimeToSurvive = surviveSomeTimeSettings.TimeToSurvive;
    }

    private void Start()
    {
        StartCoroutine(Count());
    }

    #endregion

    #region private methods

    private IEnumerator Count()
    {
        yield return new WaitForSecondsRealtime(1);
        TimeToSurvive--;
        if (TimeToSurvive <= 0) onTimerEnd.Invoke();
        else StartCoroutine(Count());
    }

    #endregion
}