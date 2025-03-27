using UnityEngine;

public class VehicleCam : MonoBehaviour
{
    public Transform target; // The vehicle to follow
    public float distance = 5.0f;
    public float height = 2.0f;
    public float rotationSpeed = 3.0f;
    public float moveSpeed = 5.0f;
    public float verticalSpeed = 2.0f;

    private float currentRotationAngle;
    private float desiredRotationAngle;
    private Quaternion currentRotation;
    private float currentHeight;

    void LateUpdate()
    {
        if (!target) return;

        // Get player input for orbit rotation
        float horizontalInput = Input.GetAxis("Mouse X");
        float verticalInput = Input.GetAxis("Mouse Y");

        desiredRotationAngle += horizontalInput * rotationSpeed;
        height += verticalInput * verticalSpeed * Time.deltaTime;
        height = Mathf.Clamp(height, 1.0f, 10.0f); // Limit height range

        // Smoothly interpolate the rotation
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, desiredRotationAngle, Time.deltaTime * rotationSpeed);
        currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Determine new camera position
        Vector3 desiredPosition = target.position - (currentRotation * Vector3.forward * distance) + Vector3.up * height;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * moveSpeed);

        // Make the camera always look at the target
        transform.LookAt(target);
    }
}
