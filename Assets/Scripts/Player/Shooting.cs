
using System;
using UnityEngine;
using UnityEngine.Events;

public class Shooting : MonoBehaviour
{
    #region variables

    [SerializeField] private GameObject blood;
    [SerializeField] private GameObject muzzleFlash;
    [SerializeField] private GameObject CrossHair;
    [SerializeField] private Transform spawnBulletPosition;
    [SerializeField] private Transform spawnMuzzleFlashPos;
    [SerializeField] private Camera camera;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip fireSound;
    [SerializeField] private float fireRate = .12f;

    private GameObject flashm;
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
            flashm = Instantiate(muzzleFlash, spawnMuzzleFlashPos.position, spawnMuzzleFlashPos.rotation);
            hitted.GetComponent<Zombie>().TakeDamage(Player.instance.Damage);
            Instantiate(blood, hit.point, Quaternion.identity);
            if (audioSource) audioSource.PlayOneShot(fireSound);
        }
    }

    #endregion
}
