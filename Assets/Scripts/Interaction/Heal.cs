using System.Collections;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public GameObject injuredPerson;
    public GameObject fiveSecondSlider;
    //public AudioSource healAudio;

    public void PersonInteract()
    {
        // healAudio.Play();
        fiveSecondSlider.SetActive(true);
        StartCoroutine(Interact());
    }

    IEnumerator Interact()
    {
        yield return new WaitForSeconds(5);
        injuredPerson.SetActive(false);
        fiveSecondSlider.SetActive(false);
    }
}
