using UnityEngine;

public class VehicleExit : MonoBehaviour
{

    public GameObject vehicleCam;
    public GameObject thePlayer;
    public GameObject liveVehicle;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            thePlayer.SetActive(true);
            vehicleCam.SetActive(false);
            liveVehicle.GetComponent<CarController>().enabled = false;
        }
    }
}
