using System.Collections.Generic;
using UnityEngine;

public class FireResetter : MonoBehaviour
{
    [SerializeField] private List<Fire> fireScripts = new List<Fire>();
    [SerializeField] private float resetIntensity = 1.0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            foreach (Fire fire in fireScripts)
            {
                if (fire != null)
                {
                    fire.Reignite(resetIntensity);
                }
            }
        }
    }
}
