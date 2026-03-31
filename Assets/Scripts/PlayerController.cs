using System;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    public InputSystem_Actions inputs;
    public float moveSpeed = 10f;
    public float rotationSpeed = 200f;
    private CharacterController controller;
    [SerializeField]private Vector2 moveInput;
    //public float gravity = -9.81f;
    public float verticalVelocity = 0f;
    public float JumpForce = 5f;
    public float pushForce = 2f;
    public bool IsDashing = false;
    public float dashForce = 20f;
    public float dashDuration = 0.5f;
    private float dashTimer = 0f;


    private void Awake()
    {
        inputs = new ();
        controller = GetComponent<CharacterController>();
    }
    private void OnEnable()
    {
        inputs.Enable();
        inputs.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputs.Player.Jump.performed += Jump_performed;
        inputs.Player.Sprint.performed += OnDash;
    }

    

    void Start()
    {
        
    }

    
    void Update()
    {
        Movement();
        //OnSimpleMovement();
    }




    public void Movement()
    {
        transform.Rotate(Vector3.up * moveInput.x * rotationSpeed * Time.deltaTime);

        Vector3 moveDir = transform.forward * moveSpeed * moveInput.y ;
        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if(controller.isGrounded && verticalVelocity < 0)
           
            verticalVelocity = -2f;

        

        moveDir.y = verticalVelocity;


        if (IsDashing)
        {
            moveDir = transform.forward * dashForce * ( dashTimer / dashTimer );
            IsDashing =false;
            dashTimer -= Time.deltaTime;
            if(dashTimer <= 0)
                IsDashing = false;

        }

        
        controller.Move(moveDir * Time.deltaTime);


    }
    public void OnSimpleMovement()
    {
        transform.Rotate(Vector3.up * moveInput.x * rotationSpeed * Time.deltaTime);

        Vector3 moveDir = transform.forward * moveSpeed * moveInput.y;
        controller.SimpleMove(moveDir);
    }


    private void Jump_performed(InputAction.CallbackContext context)
    {
        if (!controller.isGrounded) return;
        verticalVelocity = JumpForce;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
     Vector3 pushDir =(hit.transform.position - transform.position).normalized;
        if(hit.rigidbody != null)
             hit.rigidbody.AddForce(pushDir* pushForce , ForceMode.Impulse);
    }
    private void OnDash(InputAction.CallbackContext context)
    {
        IsDashing = true;
        dashTimer = dashDuration;
    }
}
