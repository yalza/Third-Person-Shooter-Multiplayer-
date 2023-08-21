using UnityEngine;

public class RunState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Running", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Mathf.Abs(movement._hzInput) >= 0.9f)
        {
            ExitState(movement, movement.Walk);
        }
        else if(movement.dir.magnitude < 0.1f)
        {
            ExitState(movement, movement.Idle);
        }

        movement.currentMoveSpeed = movement.runSpeed;
    }

    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("Running", false);
        movement.SwitchState(state);
    }
}
