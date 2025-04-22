using System.Collections;
using UnityEngine;

public class Crash : MonoBehaviour
{
    public GameObject personInCar;
    public GameObject personOnFloor;
    public GameObject vehicleDoor;
    public GameObject fiveSecondSlider;
    //public AudioSource cutAudio;

    public void PersonInteract()
    {
       // cutAudio.Play();
        fiveSecondSlider.SetActive(true);
        StartCoroutine(Interact());
    }

    IEnumerator Interact()
    {
        yield return new WaitForSeconds(5);
        vehicleDoor.SetActive(false);
        personInCar.SetActive(false);
        personOnFloor.SetActive(true);
        fiveSecondSlider.SetActive(false);
    }

}
