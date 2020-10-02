using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SurviveTimer : MonoBehaviour
{
    #region variables

    public UnityEvent onTimerEnd;

    [SerializeField] private SurviveSettings surviveSomeTimeSettings;

    private float timeToSurvive;

    #endregion

    #region Unity methods

    private void Start()
    {
        StartCoroutine(Count());
        timeToSurvive = surviveSomeTimeSettings.TimeToSurvive;
    }

    #endregion

    #region private methods

    private IEnumerator Count()
    {
        yield return new WaitForSecondsRealtime(1);
        timeToSurvive--;
        if (timeToSurvive <= 0) onTimerEnd.Invoke();
        else StartCoroutine(Count());
    }

    #endregion
}