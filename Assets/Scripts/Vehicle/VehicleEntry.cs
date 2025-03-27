using System.Collections;
using UnityEngine;

public class VehicleEntry : MonoBehaviour
{
    public GameObject vehicleCam;
    public GameObject thePlayer;
    public GameObject liveVehicle;
    public GameObject eLightController;
    public bool canEnter = false;
    public GameObject exitTrig;

    void Update()
    {
        if(canEnter == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                vehicleCam.SetActive(true);
                thePlayer.SetActive(false);
                liveVehicle.GetComponent<CarController>().enabled = true;
                eLightController.GetComponent<BlueLightController>().enabled = true;
                canEnter = false;
                thePlayer.transform.parent = this.gameObject.transform;
                StartCoroutine(ExitTrigger());
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            canEnter = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
            canEnter = false;
    }

    IEnumerator ExitTrigger()
    {
        yield return new WaitForSeconds(0.5f);
        exitTrig.SetActive(true);
    }
}
