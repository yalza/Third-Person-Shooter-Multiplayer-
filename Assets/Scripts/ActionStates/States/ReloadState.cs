using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ReloadState : ActionBaseState
{
    public override void EnterState(ActionStateManager action)
    {
        action.anim.ResetTrigger("Reload");
        action.anim.SetTrigger("Reload");
        action.ReloadWeapon();
    }

    public override void UpdateState(ActionStateManager action)
    {
    }
}
