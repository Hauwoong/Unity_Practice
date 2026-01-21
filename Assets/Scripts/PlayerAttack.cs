using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Setting")]
    [SerializeField] private int attackDamage = 3;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 0.01f;
    [SerializeField] private LayerMask enemyLayer;

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

    public void DealDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

        foreach (Collider2D hit in hits)
        {
            hit.GetComponent<EnemyHealth>()?.TakeDamage(attackDamage , movement.Playertransform);
        }
    }

    private void OnDrawGizmos()
    {
        if (attackPoint == null) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }


}
