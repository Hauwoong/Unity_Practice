using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        movement = GetComponent<PlayerMovement>();
    }

    public void TryToAttack()
    {
        if (animator == null) return;
        if (!movement.IsGrounded_Raycast()) return;

        movement.StopHorizontal();
        animator.SetTrigger("Attack");
    }

    
}
