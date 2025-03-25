using UnityEngine;

public class BlueLightController : MonoBehaviour
{
    public bool blueLightsOn;
    public GameObject blueLights;
    public GameObject sirens;
    public AudioSource hornSound;
    public AudioSource sirenSound1;
    public AudioSource sirenSound2;
    public AudioSource sirenSound3;
    public AudioSource sirenStopSound;

    private int sirenIndex = 0;
    private AudioSource currentSiren;

    void Update()
    {
        AllBlues();
        SirenControl();
    }

    void AllBlues()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            if (blueLightsOn)
            {
                blueLights.SetActive(false);
                sirens.SetActive(false);
                StopSiren();
                blueLightsOn = false;
            }
            else
            {
                blueLights.SetActive(true);
                sirens.SetActive(true);
                blueLightsOn = true;
                sirenIndex = 0; // Ensure it starts from the first siren when turned on
            }
        }
    }

    void SirenControl()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            hornSound.Play();

            if (blueLightsOn)
            {
                ToggleSiren();
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            sirenStopSound.Play();
            StopSiren();
            sirenIndex = 0; // Reset back to the first siren
        }
    }

    void ToggleSiren()
    {
        StopSiren();

        switch (sirenIndex)
        {
            case 0:
                currentSiren = sirenSound1;
                break;
            case 1:
                currentSiren = sirenSound2;
                break;
            case 2:
                currentSiren = sirenSound3;
                break;
        }

        if (currentSiren != null)
            currentSiren.Play();

        sirenIndex = (sirenIndex + 1) % 3; 
    }

    void StopSiren()
    {
        if (currentSiren != null)
            currentSiren.Stop();
        currentSiren = null; // Ensure no siren is marked as active
    }
}