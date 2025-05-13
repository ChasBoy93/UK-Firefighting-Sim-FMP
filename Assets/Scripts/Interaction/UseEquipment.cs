using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class UseEquipment : MonoBehaviour
{
    public GameObject equipmentUI;
    public GameObject useText;
    public AudioSource openLockerAudio;
    public AudioSource closeLockerAudio;
    private bool canUseEquip = false;
    void Update()
    {
        UseVehicleEquipment();
    }

    void OnTriggerEnter(Collider other)
    {
        useText.SetActive(true);
        if(other.tag == "Player")
        {
            canUseEquip = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        useText.SetActive(false);
        canUseEquip = false;
    }

    void UseVehicleEquipment()
    {
        if(canUseEquip == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                equipmentUI.SetActive(true);
                useText.SetActive(false);
                openLockerAudio.Play();
            }
        }
    }

    public void CloseEquipmentUi()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        equipmentUI.SetActive(false);
        closeLockerAudio.Play();
    }
}
