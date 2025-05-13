using UnityEngine;

public class BAToggle : MonoBehaviour
{
    public GameObject baMask;
    public AudioSource maskAudio;
    bool maskIsOn;

    void Update()
    {
        BAMask();
    }
    void BAMask()
    {
        if(Input.GetKeyUp(KeyCode.O))
        {
            if (maskIsOn)
            {
                baMask.SetActive(false);
                maskAudio.Stop();
                maskIsOn = false;
            }
            else
            {
                baMask.SetActive(true);
                maskAudio.Play();
                maskIsOn = true;
            }
        }
    }
}
