using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    #region variables

    [SerializeField] private SceneNamesSettings sceneNamesSettings;

    #endregion

    #region public methods

    public void OnResume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }

    public void OnBackToMenu()
    {
        SceneManager.LoadScene(sceneNamesSettings.MainMenuSceneName);
    }

    #endregion
}