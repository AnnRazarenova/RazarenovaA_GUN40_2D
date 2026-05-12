using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerMovement : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private Player controls;
    private Vector2 moveInput;
    private bool isGrounded;

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float groundCheckRadius = 0.3f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheckPoint;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new Player();

        rb.gravityScale = 2.5f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnEnable()
    {
        controls.PlayerSide.Enable();
        controls.PlayerSide.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.PlayerSide.Move.canceled += ctx => moveInput = Vector2.zero;
        controls.PlayerSide.Jump.performed += OnJump;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheckPoint.position, groundCheckRadius);
        }
    }
}