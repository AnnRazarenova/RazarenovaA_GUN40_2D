using UnityEngine;
public class PlayerMoveAboveIsometric : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;
    private Player controls;
    private Vector2 moveInput;

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controls = new Player();

        rb.gravityScale = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnEnable()
    {
        controls.PlayerAboveIsometric.Enable();
        controls.PlayerAboveIsometric.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.PlayerAboveIsometric.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveInput * moveSpeed;
    }
}