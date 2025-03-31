using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Extinguisher : MonoBehaviour
{
    public GameObject hose;
    public float amountExtinguishedPerSecond = 1.0f;
    private void Update()
    {
        if (Physics.Raycast(hose.transform.position, hose.transform.forward, out RaycastHit hit, 100f)
            && hit.collider.TryGetComponent(out Fire fire))
            fire.TryExtinguish(amountExtinguishedPerSecond * Time.deltaTime);
    }
}
