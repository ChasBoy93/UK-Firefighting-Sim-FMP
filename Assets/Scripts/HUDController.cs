using UnityEngine;
using TMPro;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;

    private void Awake()
    {
        instance = this;
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
}
