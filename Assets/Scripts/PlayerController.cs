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
    }

    void FixedUpdate()
    {
        UpdateMovementLock(); // 상태결정(락 여부)
        movement.FixedUpdateMove();// 물리 계산 (속도/힘)
        movement.HandleFlip(); // 물리 계산 (속도/힘)
        movement.HandleMovementAnimations(); // 애니메이션 파라미터 반영
    }

    void UpdateMovementLock()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        bool isAttacking = info.IsName("PlayerAttack");
        movement.SetCanMove(!isAttacking);
    }

}
