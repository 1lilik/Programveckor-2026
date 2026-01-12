using UnityEngine;

public class Movment : MonoBehaviour
{
    Rigidbody rb; 

    [Header("Movment")]
    public float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float jumpForce;
    public float jumpCooldown;
    public float groundDrag;
    public float airMultipiler;
    public float startYScale; 

    [Header("Ground Check")]
    public float playerHeigt;
    public LayerMask Ground;
    bool grounded;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;

    bool jumpReady = true;
    public Transform looking;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDiraction;
    public MovmentState state;
    
    public enum MovmentState
    {
        walking,
        sprinting,
        crouching,
        air,
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startYScale = transform.localScale.y; 
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeigt * 0.5f + 0.3f, Ground);

        rb.linearDamping = grounded ? groundDrag : 0;

        PlayerInput();
        SpeedControl();
        StateHandler(); 
    }

    private void FixedUpdate()
    {
        MovePlayer(); 
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && jumpReady && grounded)
        {
            jumpReady = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKeyDown(crouchKey))
        {
            StartCrouch(); 
        }

        if (Input.GetKeyUp(crouchKey))
        {
            StopCrouch();
        }
    }

    private void StateHandler()
    {
        if (grounded && Input.GetKey(crouchKey))
        {
            state = MovmentState.crouching;
            moveSpeed = crouchSpeed; 
        }
        else if (grounded && Input.GetKey(sprintKey) && !Input.GetKey(crouchKey))
        {
            state = MovmentState.sprinting;
            moveSpeed = sprintSpeed; 
        }
        else if (grounded)
        {
            state = MovmentState.walking;
            moveSpeed = walkSpeed; 
        }
        else
        {
            state = MovmentState.air; 
        }
    }

    private void MovePlayer()
    {
        moveDiraction = looking.forward * verticalInput + looking.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDiraction.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDiraction.normalized * moveSpeed * 10f * airMultipiler, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatvel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatvel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatvel.normalized * moveSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        jumpReady = true;
    }

    private void StartCrouch()
    {
        if(state != MovmentState.air)
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(-transform.up * 10);
        }
    }

    private void StopCrouch()
    {
        transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
    }

}
