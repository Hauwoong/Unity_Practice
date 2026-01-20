using UnityEngine;
using UnityEngine.Timeline;

public class AnimationEvents : MonoBehaviour
{
    private PlayerMovement player;
    private void Awake()
    {
        player = GetComponentInParent<PlayerMovement>();
    }

    public void DisableMovementAndJump()
    {
        player.SetCanMove(false);
    }

    public void EnableMovementAndJump()
    {
        player.SetCanMove(true);
    }
}
