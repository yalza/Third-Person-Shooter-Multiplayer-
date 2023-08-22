using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : ActionBaseState
{
    public override void EnterState(ActionStateManager action)
    {
        
    }

    public override void UpdateState(ActionStateManager action)
    {
        if(Input.GetKeyDown(KeyCode.R) && CanReload(action) && action.canReload)
        {
            action.SwitchState(action.Reload);
        }
    }

    private bool CanReload(ActionStateManager action)
    {
        if (action.ammo.currentAmmo == action.ammo.clipSize) return false;
        if(action.ammo.extraAmmo == 0) return false;
        return true;
    }
}
