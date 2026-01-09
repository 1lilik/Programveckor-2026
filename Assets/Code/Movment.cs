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
    public float airMulitpiler;
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
        verticalInput = Input.GetAxisRaw("VerticalInput");

        if (Input.GetKey(jumpKey) && jumpReady)
        {
            jumpReady = false;
            Jump();
            InvokeRepeating(nameof(ResetJump), jumpCooldown)
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
        if (grounded && Input.GetKeyDown(crouchKey))
        {
            state = MovmentState.crouching;
            moveSpeed = crouchSpeed; 
        }
        else if (grounded && Input.GetKey(sprintKey) && !Input.GetKey(crouchKey))
        {
            //st
        }
    }

}
