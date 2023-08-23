using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.anim.SetBool("Aiming", true);
        aim.currentFov = aim.adsFov;
    }
        

    public override void UpdateState(AimStateManager aim)
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            aim.SwitchState(aim.HipFire);
        }
    }
}
