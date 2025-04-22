using UnityEngine;

public class EquipmentButton : MonoBehaviour
{
    public GameObject theObject;

    public void Equipment()
    {
        theObject.SetActive(true);
    }
}
