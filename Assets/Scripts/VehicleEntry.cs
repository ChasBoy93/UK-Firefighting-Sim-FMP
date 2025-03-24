using UnityEngine;

public class VehicleEntry : MonoBehaviour
{
    public GameObject vehicleCam;
    public GameObject thePlayer;
    public GameObject liveVehicle;
    public bool canEnter = false;

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
}
