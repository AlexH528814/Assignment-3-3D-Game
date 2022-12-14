using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;
    


    [Header("Movement")]

    [SerializeField]
    public float speed = 5f;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [SerializeField]
    bool infiniteJumping = false;

    [Header("GroundCheck")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Other")]
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;
    }

    private void Update()
    {
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);

        MyInput();
        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }

        else if (!grounded)
        {
            rb.drag = 0;
        }


    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke("ResetJump", jumpCooldown);
        }

        else if (Input.GetKey(jumpKey) && infiniteJumping == true)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > speed)
        {
            Vector3 limitedVel = flatVel.normalized * speed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        //Resets upwards velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //Adds upwards velocity
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            //Continually adds speed * 10 units of force in moveDirection
            rb.AddForce(moveDirection.normalized * speed * 10, ForceMode.Force);
        }

        else if (!grounded)
        {
            //Continually adds speed * 10 * airMultiplier units of force in moveDirection
            rb.AddForce(moveDirection.normalized * speed * 10 * airMultiplier, ForceMode.Force);
        }
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

}