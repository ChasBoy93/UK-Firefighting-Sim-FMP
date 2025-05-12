using UnityEngine;

public class ClearWaypoint : MonoBehaviour
{
    public GameObject waypointCube;

    private void OnTriggerEnter(Collider other)
    {
        waypointCube.SetActive(false);
    }
}
