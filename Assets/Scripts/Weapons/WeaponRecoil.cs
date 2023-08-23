using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] Transform recoilFollowPos;
    [SerializeField] float kickBackAmount, kickBackSpeed, returnSpeed;
    float currentRecoilFollowPos, finalRecoilFollowPos;

    private void Update()
    {
        currentRecoilFollowPos = Mathf.Lerp(currentRecoilFollowPos,0,returnSpeed * Time.deltaTime);
        finalRecoilFollowPos = Mathf.Lerp(finalRecoilFollowPos,currentRecoilFollowPos,kickBackSpeed*Time.deltaTime);
        recoilFollowPos.localPosition = new Vector3(0, 0, finalRecoilFollowPos);
    }

    public void TriggerRecoil() => currentRecoilFollowPos += kickBackAmount;
}
