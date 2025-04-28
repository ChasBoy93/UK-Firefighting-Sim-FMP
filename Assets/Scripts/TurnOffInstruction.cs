using System.Collections.Generic;
using UnityEngine;

public class TurnOffInstruction : MonoBehaviour
{
    public GameObject buildingFireOne;
    public GameObject crashOne;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            buildingFireOne.SetActive(false);
            crashOne.SetActive(false);
        }
    }
}
