using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Referances")]
    public Rigidbody truckRB;
    public Transform[] rayPoints;
    public LayerMask driveable;
    public Transform accelerationPoint;

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
    public float acceleration = 25f;
    public float maxSpeed = 56f;
    public float deceleration = 10f;

    private Vector3 currentCarLocalVelocity = Vector3.zero;
    private float carVelocityRatio = 0;

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
    }

    void Update()
    {
        GetPlayerInput();
    }

    #endregion

    #region Movment

    void Movment()
    {
        if(isGrounded)
        {
            Acceleration();
            Decelatation();
        }
    }

    void Acceleration()
    {
        truckRB.AddForceAtPosition(acceleration * moveInput * transform.forward, accelerationPoint.position, ForceMode.Acceleration);
    }

    void Decelatation()
    {
        truckRB.AddForceAtPosition(deceleration * moveInput * -transform.forward, accelerationPoint.position, ForceMode.Acceleration);
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

    #region Input Handeling

    void GetPlayerInput()
    {
        moveInput = Input.GetAxis("Vertical");
        steerInput = Input.GetAxis("Hoizontal");
    }

    #endregion

    #region Suspension Functions

    void Suspension()
    {
        for(int i = 0; i < rayPoints.Length; i++)
        {
            RaycastHit hit;
            float maxLength = restLength + springTravel;

            if (Physics.Raycast(rayPoints[i].position, -rayPoints[i].up, out hit, maxLength + wheelRadius, driveable))
            {
                wheelsIsGrounded[i] = 1;

                float currentSpringLength = hit.distance - wheelRadius;
                float springCompression = restLength - currentSpringLength / springTravel;

                float springVelocity = Vector3.Dot(truckRB.GetPointVelocity(rayPoints[i].position), rayPoints[i].up);
                float dampForce = damperStiffness * springVelocity;

                float springForce = springStiffness * springCompression;

                float netForce = springForce - dampForce;

                truckRB.AddForceAtPosition(netForce * rayPoints[i].up, rayPoints[i].position);

                Debug.DrawLine(rayPoints[i].position, hit.point, Color.red);
            }
            else
            {
                wheelsIsGrounded[i] = 0;
                Debug.DrawLine(rayPoints[i].position, rayPoints[i].position + (wheelRadius + maxLength) * -rayPoints[i].up, Color.red);
            }
        }
    }

    #endregion
}
