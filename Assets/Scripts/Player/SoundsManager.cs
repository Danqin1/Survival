using UnityEngine;
using System.Collections;

public class SoundsManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [SerializeField] private AudioClip fire;
    [SerializeField] private AudioClip steps;

    private bool stepsPlaying;

    public void FireSound()
    {
        audioSource?.PlayOneShot(fire);
    }

    public void StepsSound()
    {
        if (!stepsPlaying)
        {
            StartCoroutine(Steps());
            stepsPlaying = true;
        }
    }

    private IEnumerator Steps()
    {
        audioSource.volume = .2f;
        audioSource?.PlayOneShot(steps);
        yield return new WaitForSecondsRealtime(steps.length);
        stepsPlaying = false;
        audioSource.volume = 1f;
    }
}