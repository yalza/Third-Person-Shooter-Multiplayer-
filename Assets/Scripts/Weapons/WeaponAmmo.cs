using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    public int clipSize;
    public int extraAmmo;
    public int currentAmmo;

    private void Start()
    {
        currentAmmo = clipSize;
    }

    private void Update()
    {
    }

    public void Reload()
    {
        if (currentAmmo != 0)
        {
            extraAmmo -= (clipSize  - currentAmmo);
            currentAmmo = clipSize;
        }
        else
        {
            if (extraAmmo >= clipSize)
            {
                extraAmmo -= clipSize;
                currentAmmo = clipSize;
            }
            else if (extraAmmo > 0)
            {
                currentAmmo = extraAmmo;
                extraAmmo = 0;
            }
        }
    }
}
