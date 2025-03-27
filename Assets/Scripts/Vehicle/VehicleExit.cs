using System.Collections;
using UnityEngine;

public class VehicleExit : MonoBehaviour
{

    public GameObject vehicleCam;
    public GameObject thePlayer;
    public GameObject liveVehicle;
    public GameObject eLightController;
    public GameObject entryTrig;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            thePlayer.SetActive(true);
            vehicleCam.SetActive(false);
            liveVehicle.GetComponent<CarController>().enabled = false;
            eLightController.GetComponent<BlueLightController>().enabled = false;
            thePlayer.transform.parent = null;
            StartCoroutine(EnterAgain());
        }
    }

    IEnumerator EnterAgain()
    {
        yield return new WaitForSeconds(0.5f);
        entryTrig.gameObject.GetComponent<BoxCollider>().enabled = true;
        this.gameObject.SetActive(false);
    }
}
