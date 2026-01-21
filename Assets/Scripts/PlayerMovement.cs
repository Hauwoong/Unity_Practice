
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
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

    Vector2 input; // 이동 방향 입력

    bool JumpPressed; 
    bool CanMove = true;
    [SerializeField] bool RightFacing = true;
    bool isGrounded;

    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D col;
    public Transform Playertransform => transform;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        animator = GetComponentInChildren<Animator>();
    }
   
    public void HandleInput()
    {
        input = Vector2.zero;

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            input.x += -1;
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            input.x += 1;
        }
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            JumpPressed = true;
        }
    }

    public void FixedUpdateMove()
    {
        isGrounded = IsGrounded_Raycast();

        if (!CanMove) return;
        
        rb.linearVelocity = new Vector2(input.x * speed, rb.linearVelocityY);
        
        if (JumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, JumpForce);
        }

        JumpPressed = false;
    }
    public void HandleFlip()
    {
        if (input.x > 0 && !RightFacing)
        {
            Filp();
        }
        else if (input.x < 0 && RightFacing)
        {
            Filp();
        }
    }

    public void HandleMovementAnimations()
    {
        if (animator == null) return;

        animator.SetFloat("xVelocity", rb.linearVelocityX);
        animator.SetFloat("yVelocity", rb.linearVelocityY);
        animator.SetBool("isGrounded", isGrounded);
    }

    public bool IsGrounded_Raycast()
    {
        if (col == null) return false;

        Vector2 origin = col.bounds.center;
        origin.y = col.bounds.min.y + 0.01f;

        RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.down, RayDistance, groundLayer);

        return hit.collider != null;
    }

    public void OnDrawGizmosSelected()
    {
        if (col == null) return;

        Gizmos.color = Color.red;

        Vector2 origin = col.bounds.center;
        origin.y = col.bounds.min.y + 0.01f;

        Gizmos.DrawLine(origin, origin + Vector2.down * RayDistance);
    }
    

    public void Filp()
    {
        transform.Rotate(0, 180, 0);
        RightFacing = !RightFacing;
    }

    public void SetCanMove(bool value)
    {
        CanMove = value;
    }

    public void StopHorizontal()
    {
        rb.linearVelocity = new Vector2(0f, rb.linearVelocityY);
    }

}
