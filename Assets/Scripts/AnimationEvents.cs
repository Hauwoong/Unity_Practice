using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    
    private PlayerAttack attack;
    private void Awake()
    {
        attack = GetComponentInParent<PlayerAttack>();
    }

    public void OnAttackHit()
    {
        attack?.DealDamage();
    }
}
