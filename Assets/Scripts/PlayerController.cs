using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Details")]
    [SerializeField] public float speed = 1.0f;
    [SerializeField] public float JumpForce = 1.0f;

    [Header("Collision Details")]
    // Overlap기법을 사용하여 착지 유무 확인
    //[SerializeField] private Transform groundcheck;
    //[SerializeField] private float groundRadius = 0.15f;

    //Raycast기법을 사용하여 착지 유무 확인
    [SerializeField] private float RayDistance = 0.1f;

    //땅 레이어 설정
    [SerializeField] private LayerMask groundLayer;

    Vector2 input;
    bool JumpPressed;

    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private bool RightFacing = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        input = Vector2.zero;

        if (Keyboard.current.aKey.isPressed) {
            input.x += -1;
        }
        if (Keyboard.current.dKey.isPressed) {
            input.x += 1;
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame) { 
            JumpPressed = true;
        }
    }

    void FixedUpdate()
    {
        HandleFlip();

        rb.linearVelocity = new Vector2(input.x * speed, rb.linearVelocityY);

        if (JumpPressed && IsGrounded_Raycast()) {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, JumpForce);
        }

        JumpPressed = false;

        HandleAnimator();
    }
    // overlap기법
    /*
    bool IsGrounded_Overlap () {
        return Physics2D.OverlapCircle(
                groundcheck.position,
                groundRadius,
                groundLayer
            );
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundcheck.position, groundRadius);
    }
    */


    // Raycast기법
    private bool IsGrounded_Raycast() {
        Collider2D col = GetComponent<Collider2D>();
        if(col == null) return false;

        Vector2 origin = col.bounds.center;
        origin.y = col.bounds.min.y + 0.01f;

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, RayDistance, groundLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmosSelected()
    {
        Collider2D col = GetComponent<Collider2D>();
        if (col == null) return;

        Gizmos.color = Color.red;

        Vector2 origin = col.bounds.center;
        origin.y = col.bounds.min.y + 0.01f;

        Gizmos.DrawLine(origin, origin + Vector2.down * RayDistance);
    }

    private void HandleAnimator() {
        bool isMoving = Mathf.Abs(rb.linearVelocityX) > 0.01f;

        animator.SetBool("isMoving", isMoving);
    }

    private void HandleFlip() {
        if (rb.linearVelocityX > 0 && !RightFacing)
        {
            Filp();
        }
        else if (rb.linearVelocityX < 0 && RightFacing){
            Filp();
        }
    }

    private void Filp() {
        transform.Rotate(0, 180, 0);
        RightFacing = !RightFacing;
    }

    
}
