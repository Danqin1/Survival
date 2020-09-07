using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject blood;
    public GameObject muzzleFlash;
    public GameObject weapon;
    public GameObject CrossHair;
    private GameObject flashm;
    public Transform spawnBulletPosition;
    public Transform spawnMuzzleFlashPos;
    Ray ray;
    RaycastHit hit;
    AudioSource audioSource;
    public AudioClip fireSound;
    public float fireRate = .1f;
    private bool isShooting = false;
    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
    }
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2,Camera.main.transform.position.z));

        if (Physics.Raycast(ray, out hit, 100f))
        {
            if (hit.transform.gameObject.CompareTag("Zombie"))
            {
                if (!isShooting)
                {
                    isShooting = true;
                    StartCoroutine(Fire(hit.transform.gameObject));
                }
            }
            else
            {
                StopShooting();
            }
        }
        else StopShooting();
        if(flashm && spawnMuzzleFlashPos)
        {
            flashm.transform.position = spawnMuzzleFlashPos.transform.position;
        }
    }
    void StopShooting()
    {
        isShooting = false;
        StopAllCoroutines();
    }
    IEnumerator Fire(GameObject hitted)
    {
        if(isShooting)
        {
            if(!hitted.GetComponent<Zombie>().isDead)
            {
                if(CrossHair.GetComponent<Animator>())
                {
                    CrossHair.GetComponent<Animator>().Play("CrossHairAnimation");
                }
                flashm = (GameObject)Instantiate(muzzleFlash, spawnMuzzleFlashPos.position, spawnMuzzleFlashPos.rotation);
                hitted.GetComponent<Zombie>().TakeDamage(Player.instance.Damage);
                Instantiate(blood, hit.point, Quaternion.identity);
                weapon.GetComponent<Animation>().Play("Ak47FireAnimation");
                if (audioSource) audioSource.PlayOneShot(fireSound);
                yield return new WaitForSeconds(fireRate);
                StartCoroutine(Fire(hitted));
            }
        } 
    }
}
