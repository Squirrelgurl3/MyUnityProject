using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlatformerControllerWAnim : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public float jumpForce = 12f;
    public float crouchSpeedMultiplier = 0.5f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Buttons")]
    public KeyCode jumpButton = KeyCode.JoystickButton0;     // Action Button 1
    public KeyCode abilityButton = KeyCode.JoystickButton1;  // Action Button 2

    [Header("Animation")]
    public Animator anim;

    [Tooltip("Exact state name in Animator Controller.")]
    public string idleAnim = "Idle";

    [Tooltip("Exact state name in Animator Controller.")]
    public string walkAnim = "Walk";

    [Tooltip("Exact state name in Animator Controller.")]
    public string crouchAnim = "Crouch";

    [Tooltip("Exact state name in Animator Controller.")]
    public string jumpAnim = "Jump";

    [Tooltip("Exact state name in Animator Controller.")]
    public string fallAnim = "Fall";

    [Tooltip("Exact state name in Animator Controller.")]
    public string abilityAnim = "Ability";

    [Header("Sprite Facing")]
    public bool flipSpriteRenderer = false;
    public SpriteRenderer spriteRendererToFlip;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isCrouching;
    private float moveInput;
    private string currentAnimState = "";

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.freezeRotation = true;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        if (anim == null)
            anim = GetComponent<Animator>();
    }

    void Update()
    {
        ReadInput();
        CheckGrounded();
        HandleCrouch();
        HandleJumpInput();
        HandleAbilityInput();
        UpdateAnimation();
        UpdateFacing();
    }

    void FixedUpdate()
    {
        ApplyHorizontalMovement();
    }

    void ReadInput()
    {
        moveInput = Input.GetAxisRaw("Horizontal");
    }

    void CheckGrounded()
    {
        if (groundCheck == null) return;

        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );
    }

    void HandleCrouch()
    {
        isCrouching = Input.GetAxisRaw("Vertical") < -0.5f;
    }

    void HandleJumpInput()
    {
        bool jumpPressed = Input.GetAxisRaw("Vertical") > 0.5f || Input.GetKeyDown(jumpButton);

        if (jumpPressed && isGrounded)
        {
            Jump();
        }
    }

    void HandleAbilityInput()
    {
        if (Input.GetKeyDown(abilityButton))
        {
            DoAbility();
        }
    }

    void ApplyHorizontalMovement()
    {
        float speed = isCrouching ? moveSpeed * crouchSpeedMultiplier : moveSpeed;
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        PlayAnimation(jumpAnim);
    }

    void DoAbility()
    {
        Debug.Log("Ability Activated!");
        PlayAnimation(abilityAnim);
    }

    void UpdateAnimation()
    {
        if (anim == null) return;

        // Air states first
        if (!isGrounded)
        {
            if (rb.linearVelocity.y > 0.1f)
            {
                PlayAnimation(jumpAnim);
                return;
            }
            else if (rb.linearVelocity.y < -0.1f)
            {
                PlayAnimation(fallAnim);
                return;
            }
        }

        // Ground states
        if (isCrouching)
        {
            PlayAnimation(crouchAnim);
            return;
        }

        if (Mathf.Abs(moveInput) > 0.01f)
        {
            PlayAnimation(walkAnim);
            return;
        }

        PlayAnimation(idleAnim);
    }

    void PlayAnimation(string stateName)
    {
        if (anim == null) return;
        if (string.IsNullOrEmpty(stateName)) return;
        if (currentAnimState == stateName) return;

        anim.Play(stateName);
        currentAnimState = stateName;
    }

    void UpdateFacing()
    {
        if (moveInput == 0) return;

        if (flipSpriteRenderer && spriteRendererToFlip != null)
        {
            spriteRendererToFlip.flipX = moveInput < 0;
        }
        else
        {
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * (moveInput < 0 ? -1 : 1);
            transform.localScale = scale;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}