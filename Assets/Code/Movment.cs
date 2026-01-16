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
    public bool canMove = true;

    bool jumpReady = true;
    bool exitSlope = false;

    [Header("Dash")]
    public float dashDistance;
    public float dashSpeed;
    public float dashCooldown;
    public bool dashReady;
    bool airDash;
    bool isDashing;
    bool haveDash;

    public UIScript uiScript;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask layerMaskWhatIsGround;
    bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    public float slopeStickForce;
    public float maxSlopeSpeedMultiplier;
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
        canMove = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.3f, layerMaskWhatIsGround);
        rb.linearDamping = grounded ? groundDrag : 0f;

        if (!canMove)
        {
            return;
        }

        PlayerInput();
        StateHandler();
        SpeedControl();

        rb.useGravity = !isDashing;
    }

    private void FixedUpdate()
    {
        if (!canMove || isDashing) return;
        MovePlayer();
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
        }
        else if (grounded)
        {
            state = MovementState.walking;
            airDash = true;
        }
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        if (OnSlope() && !exitSlope && moveDirection.magnitude < 0.1f)
        {
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, 0f);
            return;
        }

        Vector3 direction = OnSlope() && !exitSlope
            ? GetSlopeMoveDirection(moveDirection)
            : moveDirection.normalized;

        float slopeMultiplier = GetUphillSlopeMultiplier();

        if (state == MovementState.crouching)
        {
            rb.AddForce(direction * crouchSpeed * 10f * slopeMultiplier, ForceMode.Force);
        }
     
        else if (grounded)
        {
            rb.AddForce(direction * walkSpeed * 10f * slopeMultiplier, ForceMode.Force);
        }

        else
        {
            rb.AddForce(direction * walkSpeed * 10f * airMultiplier, ForceMode.Force);
        }
            
        if (OnSlope() && grounded && moveDirection.magnitude > 0.1f && !exitSlope)
        {
            rb.AddForce(Vector3.down * slopeStickForce, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        if (isDashing)
        {
            return;
        }

        Vector3 flatVel = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        float maxSpeed = walkSpeed * GetUphillSlopeMultiplier();

        if (flatVel.magnitude > maxSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limitedVel.x, rb.linearVelocity.y, limitedVel.z);
        }

        if (OnSlope() && rb.linearVelocity.y > 0f && !exitSlope)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        }
    }

    private float GetUphillSlopeMultiplier()
    {
        if (!OnSlope() || !MovingUphill() || exitSlope)
        {
            return 1f;
        }

        float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
        float t = slopeAngle / maxSlopeAngle;

        return Mathf.Lerp(1f, maxSlopeSpeedMultiplier, t * t);
    }

    private bool MovingUphill()
    {
        Vector3 slopeDir = GetSlopeMoveDirection(moveDirection);
        return Vector3.Dot(slopeDir, Vector3.down) < 0f;
    }

    private void Jump()
    {
        SoundManager.PlaySound(SoundType.PLACEHOLDER7);
        exitSlope = true;
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        jumpReady = true;
        exitSlope = false;
    }

    private void Dash()
    {
        SoundManager.PlaySound(SoundType.PLACEHOLDER6);
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
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.5f))
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
            haveDash = true;
            dashReady = true;
            other.gameObject.SetActive(false);
            uiScript.objectivesText.text = "Objective:" + uiScript.objectives[0];
        }
    }

    public void DisableMovement(float duration)
    {
        canMove = false;
        isDashing = false;
        rb.useGravity = true;

        CancelInvoke(nameof(EnableMovement));
        Invoke(nameof(EnableMovement), duration);
    }

    private void EnableMovement()
    {
        canMove = true;
    }
}
