using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStateManager : MonoBehaviour
{
    ActionBaseState currentState;

    public DefaultState Default = new DefaultState();
    public JunpState Jump = new JunpState();
    public ReloadState Reload  = new ReloadState();

    [HideInInspector] public WeaponManager weapon;
    [HideInInspector] public WeaponAmmo ammo;
    [HideInInspector] public Animator anim;

    public bool canReload = true;

    private void Start()
    {
        weapon = GetComponent<WeaponManager>();
        ammo = GetComponent<WeaponAmmo>();
        anim = GetComponent<Animator>();
        SwitchState(Default);
    }

    private void Update()
    {
        currentState.UpdateState(this);
        Debug.Log(currentState.ToString());
    }

    public void SwitchState(ActionBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    
    public void ReloadWeapon()
    {
        if (canReload)
        {
            StartCoroutine(IEReload());
        }
    }

    IEnumerator IEReload()
    {
        canReload = false;
        yield return new WaitForSeconds(2f);
        ammo.Reload();
        SwitchState(Default);
        canReload = true;
    }
    
}
