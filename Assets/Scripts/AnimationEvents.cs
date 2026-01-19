using UnityEngine;
using UnityEngine.Timeline;

public class AnimationEvents : MonoBehaviour
{
    private PlayerController player;
    private void Awake()
    {
        player = GetComponentInParent<PlayerController>();
    }

    private void DisableMovementAndJump()
    {
        player.EnalbleJumpAndMovement(false);
    }

    private void EnableMovementAndJump()
    {
        player.EnalbleJumpAndMovement(true);
    }
}
