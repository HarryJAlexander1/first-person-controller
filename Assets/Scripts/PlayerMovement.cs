using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float speed; // movement speed variable
    private float gravity = -9.81f;
    Vector3 Velocity;

    // variables to check if player is grounded (not falling)
    public Transform GroundCheckTransform;
    private float GroundDistance = 0.4f;
    public LayerMask GroundMask;
    private bool isGrounded = false;

    private float JumpHeight = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       HandleMovement();   
    }

    private void HandleMovement() 
    {
        isGrounded = Physics.CheckSphere(GroundCheckTransform.position, GroundDistance, GroundMask); // check to see if player is on the ground

        if (isGrounded && Velocity.y < 0f)
        {
            Velocity.y = -2f; // setting players velocity on y axis to -2f and not 0 to account for delay in call
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // logic for moving player along x and z axis
        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        // simulate gravity acting on player
        Velocity.y += gravity * Time.deltaTime;
        characterController.Move(Velocity * Time.deltaTime);

        // logic for jumping on y axis
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            Velocity.y += Mathf.Sqrt(JumpHeight * -2f * gravity);
        }
    }
}
