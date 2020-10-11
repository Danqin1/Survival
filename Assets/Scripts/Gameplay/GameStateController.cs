using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    #region variables

    [SerializeField] private SurviveTimer surviveTimer;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private PopupsUI popupsUI;

    #endregion

    #region Unity methods

    private void Start()
    {
        surviveTimer.onTimerEnd.AddListener(OnTimerEnd);
        Player.instance.PlayerDied.AddListener(OnPlayerDied);
        popupsUI.ShowMessage($"Survive {surviveTimer.TimeToSurvive} seconds!");
    }

    private void OnDestroy()
    {
        surviveTimer.onTimerEnd.RemoveListener(OnTimerEnd);
        Player.instance.PlayerDied.RemoveListener(OnPlayerDied);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
    }

    #endregion

    #region private methods

    private void OnTimerEnd()
    {
        popupsUI.ShowMessage($"Time ended!!!");
    }

    private void OnPlayerDied()
    {
        popupsUI.ShowMessage($"You DIED!");
    }

    #endregion
}
