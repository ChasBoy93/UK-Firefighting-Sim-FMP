using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;

    //Controls
    public GameObject controls;
    bool controlsEnabled = false;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        PlayerControlls();
    }

    [SerializeField] TMP_Text interactionText;

    public void EnableInteractionText(string text)
    {
        interactionText.text = "(F) " + text;
        interactionText.gameObject.SetActive(true);
    }
    public void DisabledInteractionText()
    {
        interactionText.gameObject.SetActive(false);
    }

    void PlayerControlls()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            controlsEnabled = !controlsEnabled; 
            controls.SetActive(controlsEnabled);
        }
    }
}
