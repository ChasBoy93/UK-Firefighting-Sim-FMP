using UnityEngine;

public class ReturnEquipment : MonoBehaviour
{
    //Equipment Objects
    public GameObject firstAidKit;
    public GameObject fireBranch;
    public GameObject cutter;
    public void ReturnTheEquipment()
    {
        firstAidKit.SetActive(false);
        fireBranch.SetActive(false);
        cutter.SetActive(false);
    }
}
