using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    public PlayerIdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Enter()
    {
        // Stop movement if any residual velocity
        if (stateMachine.RB != null)
            stateMachine.RB.linearVelocity = Vector2.zero;
        if (stateMachine.Animator != null)
            stateMachine.Animator.Play("Idle");
        Debug.Log("Entering Idle State");
    }

    public override void Tick(float deltaTime)
    {
        // --- NEW: Check for loss of ground or wall contact ---
        if (!stateMachine.IsGrounded())
        {
            // If touching wall and falling, go to WallClingState
            if (stateMachine.IsTouchingWall() && stateMachine.RB.linearVelocity.y <= 0)
            {
                stateMachine.SwitchState(stateMachine.WallClingState);
            }
            else
            {
                stateMachine.SwitchState(stateMachine.FallState);
            }
            return;
        }

        // Check for Shoot input
        if (stateMachine.InputReader.IsShootPressed()) // Use InputReader property
        {
            stateMachine.SwitchState(stateMachine.ShootState);
            return; // Exit early
        }

        // Check for Crouch input if grounded
        if (stateMachine.IsGrounded() && stateMachine.InputReader.IsCrouchHeld()) // Use InputReader property
        {
            stateMachine.SwitchState(stateMachine.CrouchState);
            return; // Exit early after state switch
        }

        // Check for Jump input
        if (stateMachine.InputReader.IsJumpPressed())
        {
            Debug.Log($"[IdleState] Jump pressed. IsGrounded: {stateMachine.IsGrounded()} JumpsRemaining: {stateMachine.JumpsRemaining}");
            // Always allow jump if grounded
            stateMachine.SwitchState(stateMachine.JumpState);
            return;
        }


        // Check for movement input to transition to Walk/Run
        Vector2 moveInput = stateMachine.InputReader.GetMovementInput(); // Use InputReader property
        if (moveInput != Vector2.zero)
        {
            if (stateMachine.InputReader.IsRunPressed()) // Use InputReader property
                stateMachine.SwitchState(stateMachine.RunState);
            else
                stateMachine.SwitchState(stateMachine.WalkState);
        }
    }

    public override void Exit()
    {
         Debug.Log("Exiting Idle State");
    }
}