using UnityEngine;

public class BlueLightController : MonoBehaviour
{
    public bool blueLightsOn;
    public GameObject blueLights;
    //public GameObject redLights;
    public GameObject sirens;

    void Update()
    {
        AllBlues();
    }

    void AllBlues()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (blueLightsOn)
            {
                blueLights.SetActive(false);
                sirens.SetActive(false);
                blueLightsOn = false;
            }
            else
            {
                blueLights.SetActive(true);
                sirens.SetActive(true);
                blueLightsOn = true;
            }
        }
    }
}
