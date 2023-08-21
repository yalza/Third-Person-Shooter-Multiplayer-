using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : AimBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.anim.SetBool("Shooting", true);
    }

    public override void UpdateState(AimStateManager aim)
    {
        
    }

    void ExitState()
    {

    }
}
