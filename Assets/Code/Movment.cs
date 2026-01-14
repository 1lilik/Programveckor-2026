using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Movment : MonoBehaviour
{
    public Rigidbody rb;

    [Header("Movement")]
    public float walkSpeed;
    public float jumpForce;
    public float jumpCooldown;
    public float groundDrag;
    public float airMultiplier;
    public bool canMove;

    bool jumpReady = true;

    [Header("Dash")]
    public float dashDistance;
    public float dashSpeed;
    public float dashCooldown;
    public bool dashReady = false;
    bool airDash;
    bool isDashing;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask Ground;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    RaycastHit slopeHit;

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
        rb.freezeRotation = true;
        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, Ground);

        rb.linearDamping = grounded ? groundDrag : 0f;

        PlayerInput();
        StateHandler();
        SpeedControl();

        if (OnSlope() && !isDashing)
        {
            rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
        }
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
        if (canMove == true)
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
                if (!grounded && airDash)
                {
                    Dash();
                    airDash = false;
                }
                else if (grounded)
                {
                    Dash();
                }
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
            airDash = true;
            canMove = true;
        }
        else if (grounded)
        {
            state = MovementState.walking;
            airDash = true;
            canMove = true;
        }
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        Vector3 direction;

        if (OnSlope())
            direction = GetSlopeMoveDirection(moveDirection);
        else
            direction = moveDirection.normalized;

        if (state == MovementState.crouching)
        {
            rb.AddForce(direction * crouchSpeed * 10f, ForceMode.Force);
            return;
        }

        if (grounded)
        {
            rb.AddForce(direction * walkSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(direction * walkSpeed * 10f * airMultiplier, ForceMode.Force);
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

    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down,
            out slopeHit, playerHeight * 0.5f + 0.5f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    private Vector3 GetSlopeMoveDirection(Vector3 direction)
    {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Dash Power Up"))
        {
            dashReady = true;
            Destroy(other.gameObject);
        }

        if (other.CompareTag("JumpPad"))
        {
            canMove = false;
            Debug.Log("YES");
        }
    }
}
