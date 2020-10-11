using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopupsUI : MonoBehaviour
{
    #region variables

    [SerializeField] private GameObject popup;
    [SerializeField] private int popupDuration;

    #endregion

    #region Unity methods

    private void Awake()
    {
        popup.SetActive(false);
    }

    #endregion

    #region public methods

    public void ShowMessage(string message)
    {
        popup.SetActive(true);
        popup.GetComponentInChildren<Text>().text = message;
        StartCoroutine(ClosePopup());
    }

    #endregion

    #region private methods

    private IEnumerator ClosePopup()
    {
        yield return new WaitForSeconds(popupDuration);
        popup.SetActive(false);
    }

    #endregion
}