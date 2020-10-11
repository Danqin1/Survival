 using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    #region variables

    [SerializeField] private SceneNamesSettings sceneNamesSettings;
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject settingsPanel;

    #endregion

    #region public methods

    public void OnPlayBtn()
    {
        SceneManager.LoadScene(sceneNamesSettings.MainSceneName);
        Time.timeScale = 1;
    }

    public void OnSettingsBtn()
    {
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void OnQuitBtn()
    {
        Application.Quit();
    }

    public void OnBackBtn()
    {
        settingsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    #endregion
}
