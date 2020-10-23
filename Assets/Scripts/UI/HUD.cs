using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField] private GameObject dmgScreen;
    [SerializeField] private Slider HPBar;

    private void Start()
    {
        Player.instance.TakingDamage.AddListener(OnPlayerTakingDamage);
        HPBar.value = Player.instance.Health;
    }

    private void OnDestroy()
    {
        Player.instance.TakingDamage.RemoveListener(OnPlayerTakingDamage);
    }

    private void OnPlayerTakingDamage()
    {
        dmgScreen.GetComponent<Animation>().Play();
        HPBar.value = Player.instance.Health / 100;
    }
}
