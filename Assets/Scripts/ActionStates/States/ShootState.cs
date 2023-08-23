using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootState : ActionBaseState
{
    public override void EnterState(ActionStateManager action)
    {
        action.anim.SetBool("Shooting", true);
    }

    public override void UpdateState(ActionStateManager action)
    {
        if(Input.GetKeyUp(KeyCode.Mouse0)||action.ammo.currentAmmo == 0)
        {
            action.anim.SetBool("Shooting", false);
            action.SwitchState(action.Default);

        }
        if(action.canReload && action.weapon.ShouldFire())
        {
            action.weapon.Fire();
            action.recoil.TriggerRecoil();
        }
    }
}
