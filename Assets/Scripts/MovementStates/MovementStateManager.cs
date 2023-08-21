using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    #region Movement

    public float currentMoveSpeed = 1.2f;
    public float hzSpeed = 0.6f;
    public float frontSpeed = 1.2f;
    public float backSpeed = 0.6f;
    public float runSpeed = 2f;

    [HideInInspector] public float _hzInput, _vtInput;
    Vector3 velocity;
    [HideInInspector] public Vector3 dir;
    CharacterController controller;
    #endregion

    #region Check Grounded
    Vector3 spherePos;
    [SerializeField] float groundYOffset;
    [SerializeField] LayerMask groundMask;
    #endregion

    #region Gravity  
    public float gravity = -9.81f;
    #endregion

    [HideInInspector] public Animator anim;

    MovementBaseState currentState;

    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public RunState Run = new RunState();

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        SwitchState(Idle);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        GetDirectionAndMove();
        Gravity();

        currentState.UpdateState(this);

        anim.SetFloat("hzInput", _hzInput);
        anim.SetFloat("vtInput", _vtInput);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void GetDirectionAndMove()
    {
        _hzInput = Input.GetAxis("Horizontal");
        _vtInput = Input.GetAxis("Vertical");

        dir = transform.forward * _vtInput + transform.right* _hzInput;

        controller.Move(dir.normalized * currentMoveSpeed * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x,transform.position.y - groundYOffset,transform.position.z);
        return Physics.CheckSphere(spherePos, controller.radius - 0.05f, groundMask);
    }

    private void Gravity()
    {
        if(IsGrounded() == false)
        {
            velocity.y += gravity * Time.deltaTime;
        }
        else if(velocity.y < 0)
        {
            velocity.y = -2;
        }

        controller.Move(velocity * Time.deltaTime);
    }

}
