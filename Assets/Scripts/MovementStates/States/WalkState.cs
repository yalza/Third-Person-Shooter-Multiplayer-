using UnityEngine;

public class WalkState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Walking", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && movement._vtInput > 0f)
        {
            ExitState(movement,movement.Run);
        }
        else if(movement.dir.magnitude < 0.1f)
        {
            ExitState(movement,movement.Idle);
        }

        if (movement._vtInput < 0f) movement.currentMoveSpeed = movement.backSpeed;
        else if (movement._vtInput > 0f) movement.currentMoveSpeed = movement.frontSpeed;
        if (Mathf.Abs(movement._hzInput) > 0) movement.currentMoveSpeed = movement.hzSpeed; 
    }

    void ExitState(MovementStateManager movement,MovementBaseState state)
    {
        movement.anim.SetBool("Walking", false);
        movement.SwitchState(state);
    }
}
