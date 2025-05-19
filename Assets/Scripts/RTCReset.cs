using UnityEngine;

public class RTCReset : MonoBehaviour
{
    public GameObject vehicleDoor;
    public GameObject manInVehicle;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Y))
        {
            vehicleDoor.SetActive(true);
            manInVehicle.SetActive(true);
        }
    }
}
