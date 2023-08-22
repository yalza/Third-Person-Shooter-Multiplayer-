using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon Properties")]
    [SerializeField] GameObject mfx;

    [Header("Fire Rate")]
    [SerializeField] float fireRate;
    float fireRateTimer;

    [Header("Bullet Properties")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform barrelPos;
    [SerializeField] float bulletVelocity;
    AimStateManager aim;
    WeaponAmmo ammo;


    private void Start()
    {
        aim = GetComponent<AimStateManager>();
        ammo = GetComponent<WeaponAmmo>();
        fireRateTimer = 0;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) && ammo.currentAmmo >0)
        {
            aim.anim.SetBool("Shooting", true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) || ammo.currentAmmo ==0)
        {
            aim.anim.SetBool("Shooting", false);
        }
        if (ShouldFire()) Fire();
        
    }

    bool ShouldFire()
    {
        fireRateTimer += Time.deltaTime;
        if (fireRateTimer < fireRate) return false;
        if (ammo.currentAmmo == 0) return false;
        if (Input.GetKey(KeyCode.Mouse0)) return true;
        return false;
    }

    void Fire()
    {
        #region mfx
        GameObject mfxTmp = ObjectPooling.Instant.GetGameObject(mfx);
        mfxTmp.transform.position = barrelPos.position;
        mfxTmp.transform.rotation = barrelPos.rotation;
        mfxTmp.SetActive(true);
        StartCoroutine(DisActive(mfxTmp));
        #endregion

        #region bullet
        fireRateTimer = 0;
        barrelPos.LookAt(aim.aimPos);
        GameObject bulletTmp = ObjectPooling.Instant.GetGameObject(bullet);
        bulletTmp.transform.position = barrelPos.position;
        bulletTmp.transform.rotation = barrelPos.rotation;
        bulletTmp.SetActive(true);

        Rigidbody rg = bulletTmp.GetComponent<Rigidbody>();
        rg.velocity = barrelPos.forward * bulletVelocity;
        #endregion

        ammo.currentAmmo--;
        
    }

    IEnumerator DisActive(GameObject mfx)
    {
        yield return new WaitForSeconds(0.2f);
        mfx.SetActive(false);
    }


}
