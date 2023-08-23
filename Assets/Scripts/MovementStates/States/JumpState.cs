using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.ResetTrigger("Jump");
        movement.anim.SetTrigger("Jump");
        movement.Jump();
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (movement.IsGrounded())
        {
            movement.SwitchState(movement.prevState);
        }
    }
}
