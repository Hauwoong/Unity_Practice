using UnityEngine;

public class EnemyKnockback : MonoBehaviour
{
    [SerializeField] private float force = 5.0f;
    [SerializeField] private float UpwardForce = 1f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockBack(Transform attacker)
    {

        if (attacker == null) return;

        float dirX = rb.position.x - attacker.position.x;
        float horizontalDir = Mathf.Sign(dirX);

        rb.linearVelocity = Vector2.zero;

        rb.AddForce(new Vector2(horizontalDir * force, UpwardForce), ForceMode2D.Impulse);
    }
}
