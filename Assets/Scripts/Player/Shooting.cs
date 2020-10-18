using UnityEngine;

public class Shooting : MonoBehaviour
{
    #region variables

    [SerializeField] private GameObject blood;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject CrossHair;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform spawnMuzzleFlashPos;
    [SerializeField] private Camera camera;
    [SerializeField] private SoundsManager soundsManager;
    [SerializeField] private float fireRate = .12f;

    private const string ZOMBIE_TAG = "Zombie";
    private const string HEAD_TAG = "Head";

    private GameObject muzzleFlashObject;
    private Ray ray;
    private RaycastHit hit;
    private float previousTime = 0;

    #endregion

    #region Unity methods

    void Update()
    {
        ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, camera.transform.position.z));

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.transform.gameObject.GetComponent<Zombie>() != null)
            {
                if (Time.realtimeSinceStartup - previousTime > fireRate)
                {
                    Fire(hit.transform.gameObject, hit.transform.gameObject.tag);
                    previousTime = Time.realtimeSinceStartup;
                }
            }
        }
        if (muzzleFlashObject && spawnMuzzleFlashPos)
        {
            muzzleFlashObject.transform.position = spawnMuzzleFlashPos.transform.position;
        }
    }

    #endregion

    #region private methods

    private void Fire(GameObject hitted, string tag)
    {
        if (!hitted.GetComponent<Zombie>().IsDead)
        {
            if (CrossHair.GetComponent<Animator>())
            {
                CrossHair.GetComponent<Animator>().Play("CrossHairAnimation");
            }
            muzzleFlashObject = Instantiate(muzzleFlash, spawnMuzzleFlashPos.position, spawnMuzzleFlashPos.rotation);
            hitted.GetComponent<Zombie>().TakeDamage((tag == ZOMBIE_TAG) ? Player.instance.Damage : Player.instance.HeadDamage);
            Instantiate(blood, hit.point, Quaternion.identity);
            soundsManager.FireSound();

            if (tag == HEAD_TAG) Debug.Log("Head!");
        }
    }

    #endregion
}
