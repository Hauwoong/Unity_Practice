using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public float JumpForce = 1.0f;
    Rigidbody2D rb;
    Vector2 input;
    bool JumpPressed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.linearVelocity = new Vector2(input.x * speed, rb.linearVelocityY);

        if (JumpPressed && IsGrouded()) {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, JumpForce);
        }

        JumpPressed = false;
    }

    bool IsGrouded() {
        return Mathf.Abs(rb.linearVelocity.y) < 0.01f;
    }
}
