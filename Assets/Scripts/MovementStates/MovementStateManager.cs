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
    public float jumpForce = 10f;

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

    public WalkState Walk = new WalkState();
    public RunState Run = new RunState();
    public JumpState JumpS = new JumpState();

    public MovementBaseState prevState;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
  

        SwitchState(Walk);
        prevState = Walk;

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
        prevState = currentState;
        currentState = state;
        currentState.EnterState(this);
    }

    private void GetDirectionAndMove()
    {
        _hzInput = Input.GetAxis("Horizontal");
        _vtInput = Input.GetAxis("Vertical");

        dir = transform.forward * _vtInput + transform.right* _hzInput;

        controller.Move((dir.normalized * currentMoveSpeed)* Time.deltaTime);
    }

    public bool IsGrounded()
    {
        /*spherePos = new Vector3(transform.position.x,transform.position.y - groundYOffset,transform.position.z);
        return !Physics.CheckSphere(spherePos, controller.radius - 0.1f, groundMask);*/

        return controller.isGrounded;

    }

    private void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }



    public void Jump()
    {
        velocity.y = Mathf.Sqrt(2 * jumpForce * -gravity);
    }

}
