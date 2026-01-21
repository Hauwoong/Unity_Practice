using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    private int currentHp;

    EnemyKnockback enemyknockback;

    private void Awake()
    {
        currentHp = maxHP;
        enemyknockback = GetComponent<EnemyKnockback>();
    }

    public void TakeDamage(int damage, Transform attacker)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            Die();
            return;
        }

        else
        {
            enemyknockback?.ApplyKnockBack(attacker);
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
