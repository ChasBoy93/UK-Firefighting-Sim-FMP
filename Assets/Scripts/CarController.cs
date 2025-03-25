using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Audio;

public class CarController : MonoBehaviour
{
    [Header("Referances")]
    public Rigidbody truckRB;
    public Transform[] rayPoints;
    public LayerMask driveable;
    public Transform accelerationPoint;
    public GameObject[] tires = new GameObject[4];
    public GameObject[] frontTireParents = new GameObject[2];
    public AudioSource engineSound;
    public bool engineOn = false;
    public AudioSource startStopAudio; 
    public AudioClip engineStartClip;
    public AudioClip engineStopClip;

    [Header("Suspension Settings")]
    public float springStiffness;
    public float damperStiffness;
    public float restLength;
    public float springTravel;
    public float wheelRadius;

    private int[] wheelsIsGrounded = new int[4];
    private bool isGrounded = false;

    [Header("Input")]
    private float moveInput = 0;
    private float steerInput = 0;

    [Header("Car Settings")]
    public float acceleration = 12.5f;
    public float maxSpeed = 56f;
    public float deceleration = 10f;
    public float steerStrength = 15f;
    public AnimationCurve turningCurve;
    public float dragCoefficient = 1f;

    private Vector3 currentCarLocalVelocity = Vector3.zero;
    private float carVelocityRatio = 0;

    [Header("Visuals")]
    public float tireRotSpeed = 3000f;
    public float maxSteeringAngle = 30f;

    [Header("Audio")]
    [SerializeField]
    [Range(0, 1)] private float minPitch = 1f;

    [SerializeField]
    [Range(1, 5)] private float maxPitch = 5f;

    #region Unity Functions

    void Start()
    {
        truckRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Suspension(); 
        GroundCheck();
        CalculateCarVelocity();
        Movment();
        Visuals();
        EngineSound();
    }

    void Update()
    {
        GetPlayerInput();
        ToggleEngineSound();
    }

    #endregion

    #region Input Handeling

    void GetPlayerInput()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Horizontal");
    }

    #endregion

    #region Movment

    void Movment()
    {
        if(isGrounded)
        {
            Acceleration();
            Decelatation();
            Turn();
            SidewaysDrag();
        }
    }

    void Acceleration()
    {
        if (currentCarLocalVelocity.z < maxSpeed)
        {
            truckRB.AddForceAtPosition(acceleration * -moveInput * transform.forward, accelerationPoint.position, ForceMode.Acceleration);
        }
        
    }

    void Decelatation()
    {
         truckRB.AddForceAtPosition(deceleration * -moveInput * -transform.forward, accelerationPoint.position, ForceMode.Acceleration);

    }

    void Turn()
    {
        truckRB.AddTorque(steerStrength * steerInput * turningCurve.Evaluate(Mathf.Abs(carVelocityRatio)) * transform.up, ForceMode.Acceleration);
    }

    void SidewaysDrag()
    {
        float currentSidewaysSpeed = currentCarLocalVelocity.x;

        float dragMagnitude = -currentSidewaysSpeed * dragCoefficient;

        Vector3 dragForce = transform.right * dragMagnitude;

        truckRB.AddForceAtPosition(dragForce, truckRB.worldCenterOfMass, ForceMode.Acceleration);
    }

    #endregion

    #region Visuals

    void Visuals()
    {
        TireVisuals();
    }
    void TireVisuals()
    {
        float steeringAngle = maxSteeringAngle * steerInput;
        for (int i = 0; i < tires.Length; i++)
        {
            if (i < frontTireParents.Length) 
            {
                frontTireParents[i].transform.localEulerAngles = new Vector3(frontTireParents[i].transform.localEulerAngles.x, steeringAngle, frontTireParents[i].transform.localEulerAngles.z);
            }

            tires[i].transform.Rotate(Vector3.right, tireRotSpeed * (i > 2 ? carVelocityRatio : moveInput) * Time.deltaTime, Space.Self);
        }
    }

    void SetTirePosition(GameObject tire, Vector3 targetPosition)
    {
        tire.transform.position = targetPosition;
    }

    #endregion

    #region Audio

    void ToggleEngineSound()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (engineOn)
            {
                // Stop the looping engine sound immediately
                engineSound.Stop();
                engineOn = false;

                // Play stop sound
                startStopAudio.PlayOneShot(engineStopClip);
            }
            else
            {
                // Play start sound
                startStopAudio.PlayOneShot(engineStartClip);

                // Start the engine loop after the start sound finishes
                StartCoroutine(StartEngineAfterDelay(engineStartClip.length));
            }
        }
    }

    IEnumerator StartEngineAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        engineSound.Play();
        engineOn = true;
    }
    void EngineSound()
    {
        engineSound.pitch = Mathf.Lerp(minPitch, maxPitch, Mathf.Abs(carVelocityRatio));
    }

    #endregion

    #region Check Status Truck

    void GroundCheck()
    {
        int tempGroundedWheels = 0;

        for (int i = 0; i < wheelsIsGrounded.Length; i++)
        {
            tempGroundedWheels += wheelsIsGrounded[i];
        }

        if(tempGroundedWheels > 1)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void CalculateCarVelocity()
    {
        currentCarLocalVelocity = transform.InverseTransformDirection(truckRB.linearVelocity);
        carVelocityRatio = currentCarLocalVelocity.z / maxSpeed;


    }

    #endregion


    #region Suspension Functions

    void Suspension()
    {
        for(int i = 0; i < rayPoints.Length; i++)
        {
            RaycastHit hit;
            float maxDistance = restLength + springTravel;

            if (Physics.Raycast(rayPoints[i].position, -rayPoints[i].up, out hit, maxDistance + wheelRadius, driveable))
            {
                wheelsIsGrounded[i] = 1;

                float currentSpringLength = hit.distance - wheelRadius;
                float springCompression = restLength - currentSpringLength / springTravel;

                float springVelocity = Vector3.Dot(truckRB.GetPointVelocity(rayPoints[i].position), rayPoints[i].up);
                float dampForce = damperStiffness * springVelocity;

                float springForce = springStiffness * springCompression;

                float netForce = springForce - dampForce;

                truckRB.AddForceAtPosition(netForce * rayPoints[i].up, rayPoints[i].position);

                // Visuals
                SetTirePosition(tires[i], hit.point + rayPoints[i].up * wheelRadius);

                Debug.DrawLine(rayPoints[i].position, hit.point, Color.red);
            }
            else
            {
                wheelsIsGrounded[i] = 0;

                // Visuals
                SetTirePosition(tires[i], rayPoints[i].position - rayPoints[i].up * maxDistance);

                Debug.DrawLine(rayPoints[i].position, rayPoints[i].position + (wheelRadius + maxDistance) * -rayPoints[i].up, Color.red);
            }
        }
    }

    #endregion
}
