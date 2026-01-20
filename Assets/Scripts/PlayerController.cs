using Unity.Properties;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement;
    private PlayerAttack attack;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        movement.HandleInput();

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            attack.TryToAttack();
        }

        UpdateMovementLock();
    }

    void FixedUpdate()
    {
        movement.FixedUpdateMove();
        movement.HandleFlip();
        movement.HandleMovementAnimations();
    }

    void UpdateMovementLock()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        bool isAttacking = info.IsName("PlayerAttack");
        movement.SetCanMove(!isAttacking);
    }

}
