using UnityEngine;
using TMPro;

public class waypointUI : MonoBehaviour
{
    public Transform target; // Target to follow (player or truck)
    public Camera[] cameras; // Assign both MainCamera and TruckCamera
    public RectTransform uiElement; // The UI Image or its parent RectTransform
    public TMP_Text distanceText;

    public float verticalOffset = 2f; // Adjust for height above the target

    private bool isVisible = true;

    void Update()
    {
        if (target == null || !isVisible)
        {
            uiElement.gameObject.SetActive(false);
            return;
        }

        Vector3 worldPos = target.position + Vector3.up * verticalOffset;

        foreach (Camera cam in cameras)
        {
            if (cam != null && cam.gameObject.activeInHierarchy)
            {
                Vector3 screenPos = cam.WorldToScreenPoint(worldPos);

                bool onScreen = screenPos.z > 0;
                uiElement.gameObject.SetActive(onScreen);

                if (onScreen)
                {
                    uiElement.position = screenPos;
                    float distance = Vector3.Distance(cam.transform.position, target.position);
                    distanceText.text = $"{distance:F1}m";
                }

                break; // Only use the first active camera
            }
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void ToggleVisibility(bool state)
    {
        isVisible = state;
        uiElement.gameObject.SetActive(state);
    }
}
