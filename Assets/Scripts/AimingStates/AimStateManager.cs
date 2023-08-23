using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    AimBaseState currentState;

    public AimState Aim = new AimState();
    public HipFireState HipFire = new HipFireState();

    public float xAxis, yAxis;
    [SerializeField] private float _mouseSense;
    [SerializeField] Transform cameraFollowPos;

    [HideInInspector] public Animator anim;
    [HideInInspector] public CinemachineVirtualCamera vCam;

    public float adsFov = 40;
    [HideInInspector] public float hipFov;
    [HideInInspector] public float currentFov;
    public float fovSmoothSpeed = 10f;

    public Transform aimPos;
    [SerializeField] float aimSmoothSpeed = 20;
    [SerializeField] LayerMask aimMask;


    private void Start()
    {
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        anim = GetComponent<Animator>();
        SwitchState(HipFire);
    }

    private void Update()
    {
        xAxis += Input.GetAxisRaw("Mouse X") * _mouseSense;
        yAxis += Input.GetAxisRaw("Mouse Y") * _mouseSense;
        yAxis = Mathf.Clamp(yAxis, -15, 30);
        
        currentState.UpdateState(this);
        vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

        Vector2 screenCenter = new Vector2(Screen.width/2,Screen.height/2);
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if(Physics.Raycast(ray,out RaycastHit hit,Mathf.Infinity,aimMask))
        {
            aimPos.position = Vector3.Lerp(aimPos.position,hit.point,aimSmoothSpeed* Time.deltaTime);
        }
    }

    private void LateUpdate()
    {
        cameraFollowPos.localEulerAngles = new Vector3(-yAxis, cameraFollowPos.localEulerAngles.y, cameraFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, xAxis, transform.eulerAngles.z);
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
