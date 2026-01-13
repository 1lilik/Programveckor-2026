using UnityEngine;

public class Movment : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Movement")]
    public float walkSpeed;
    public float jumpForce;
    public float jumpCooldown;
    public float groundDrag;
    public float airMultiplier;

    bool jumpReady = true;

    [Header("Dash")]
    public float dashDistance;     
    public float dashSpeed;        
    public float dashCooldown;
    public bool dashReady = false; 
    bool isDashing;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask Ground;
    bool grounded;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode dashKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;

    float startYScale;

    public Transform looking;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Vector3 dashDirection;

    public MovementState state;

    public enum MovementState
    {
        walking,
        crouching,
        air,
        dashing
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, Ground);

        rb.linearDamping = grounded ? groundDrag : 0f;

        PlayerInput();
        StateHandler();
        SpeedControl();
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            MovePlayer();
        }
    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = looking.forward * verticalInput + looking.right * horizontalInput;

        if (Input.GetKey(jumpKey) && jumpReady && grounded)
        {
            jumpReady = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKeyDown(dashKey) && dashReady && moveDirection.magnitude > 0.1f)
        {
            Dash();
        }

        if (Input.GetKeyDown(crouchKey) && grounded)
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
        if (isDashing)
        {
            state = MovementState.dashing;
        }
        else if (grounded && Input.GetKey(crouchKey))
        {
            state = MovementState.crouching;
        }
        else if (grounded)
        {
            state = MovementState.walking;
        }
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        if (state == MovementState.crouching)
        {
            rb.AddForce(moveDirection.normalized * crouchSpeed * 10f, ForceMode.Force);
            return;
        }

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * walkSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * walkSpeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        if (isDashing) return;

        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        if (flatVel.magnitude > walkSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * walkSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        jumpReady = true;
    }

    private void Dash()
    {
        rb.useGravity = false; 
        dashReady = false;
        isDashing = true;

        dashDirection = moveDirection.normalized;

        float dashDuration = dashDistance / dashSpeed;

        rb.linearVelocity = dashDirection * dashSpeed;

        Invoke(nameof(EndDash), dashDuration);
        Invoke(nameof(ResetDash), dashCooldown);
    }

    private void EndDash()
    {
        isDashing = false;
        rb.useGravity = true;
    }

    private void ResetDash()
    {
        dashReady = true;
    }

    private void StartCrouch()
    {
        transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
    }

    private void StopCrouch()
    {
        transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == ("Dash Power Up"))
        {
            dashReady = true;
            Destroy(other.gameObject); 
        }
    }
}
