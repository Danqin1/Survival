using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject dmgScreen;

    private void Start()
    {
        Player.instance.TakingDamage.AddListener(OnPlayerTakingDamage);
    }

    private void OnDestroy()
    {
        Player.instance.TakingDamage.RemoveListener(OnPlayerTakingDamage);
    }

    private void OnPlayerTakingDamage()
    {
        dmgScreen.GetComponent<Animation>().Play();
    }
}
