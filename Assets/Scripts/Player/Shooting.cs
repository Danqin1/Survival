using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    #region variables

    [SerializeField] private GameObject blood;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject CrossHair;
    [SerializeField] private GameObject flashm;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform spawnMuzzleFlashPos;

    private Ray ray;
    private RaycastHit hit;
    private AudioSource audioSource;
    public AudioClip fireSound;
    private float fireRate = .12f;
    private float previousTime = 0;

    #endregion

    #region Unity methods

    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.transform.position.z));

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.transform.gameObject.CompareTag("Zombie"))
            {
                if (Time.realtimeSinceStartup - previousTime > fireRate)
                {
                    Fire(hit.transform.gameObject);
                    previousTime = Time.realtimeSinceStartup;
                }
            }
        }
        if (flashm && spawnMuzzleFlashPos)
        {
            flashm.transform.position = spawnMuzzleFlashPos.transform.position;
        }
    }

    #endregion

    #region private methods

    private void Fire(GameObject hitted)
    {
        if (!hitted.GetComponent<Zombie>().IsDead)
        {
            if (CrossHair.GetComponent<Animator>())
            {
                CrossHair.GetComponent<Animator>().Play("CrossHairAnimation");
            }
            flashm = (GameObject)Instantiate(muzzleFlash, spawnMuzzleFlashPos.position, spawnMuzzleFlashPos.rotation);
            hitted.GetComponent<Zombie>().TakeDamage(Player.instance.Damage);
            Instantiate(blood, hit.point, Quaternion.identity);
            weapon.GetComponent<Animation>().Play("Ak47FireAnimation");
            if (audioSource) audioSource.PlayOneShot(fireSound);
        }
    }

    #endregion
}
