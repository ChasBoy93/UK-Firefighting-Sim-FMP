using UnityEngine;

public class ReturnEquipment : MonoBehaviour
{
    //Equipment Objects
    public GameObject firstAidKit;
    public void ReturnTheEquipment()
    {
        firstAidKit.SetActive(false);
    }
}
