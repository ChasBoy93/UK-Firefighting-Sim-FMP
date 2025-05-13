using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovment : MonoBehaviour
{

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    private bool hoseOn;
    public GameObject hoseObject;
    public AudioSource hoseSound;

    void Update()
    {
        UseHose();
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    void UseHose()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (hoseOn)
            {
                hoseOn = false;
                hoseObject.SetActive(false);
                hoseSound.Stop();

            }
            else
            {
                hoseOn = true;
                hoseObject.SetActive(true);
                hoseSound.Play();
            }
        }
    }
}
