using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent; // this will move the player through offmeshlinks, allowing the player to pass between rooms using gates
    [SerializeField] private PlayerStats myStats;
    CharacterController controller;

    public Vector3 move;
    public Vector3 dashForward;
    public float targetAngle;
    public float angle;
    public float minSpeed = 3.5f;
    public float maxSpeed = 7f;
    
    public float speed;
    public float dashSpeed;
    public bool IsDashing;
    
    public float jumpSpeed = 3f;
    public float gravity = -20f;
    public float groundDistance = 0.1f;
    public Transform groundChecker;
    public LayerMask groundMask;
    [SerializeField] bool isGrounded;
    [SerializeField] Vector3 velocity;

    public Quaternion currentRotation;
    public Quaternion targetRotation;
    float turnSmoothness = 0.1f;
    float turnSmoothVelocity;
    float turnRate = 720f;

    PlayerCombat combat;
    public bool isAttacking = false;
    public bool isMoving = false;

    InteractableScanner scanner;



    private void OnEnable()
    {
        AttackStringState.OnAttackAnimationPlayRequest += OnAttack;
        PlayerStats.OnStatChange += UpdateMoveSpeed;
        // GatePlatform.OnPass += OnPass;
    }
    private void OnDisable()
    {
        AttackStringState.OnAttackAnimationPlayRequest -= OnAttack;
        PlayerStats.OnStatChange -= UpdateMoveSpeed;
        // GatePlatform.OnPass -= OnPass;
    }

    /*private void OnPass(GatePlatform gate)
    {
        agent.SetDestination(gate.interactionTransform.position);
    }*/


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        scanner = GetComponent<InteractableScanner>();
        combat = GetComponent<PlayerCombat>();
        maxSpeed = myStats.Movespeed.GetValue();
        dashSpeed = 0;
        dashForward = Vector3.zero;
        IsDashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);
        if (isGrounded)
        {
            velocity.y = -1f;
        }

        HandleRotation();
        HandleMovement(controller);
    }

    void HandleRotation()
    {
        if (move.magnitude != 0f)
        {
            Vector3 positionToLookAt;
            positionToLookAt.x = move.x;
            positionToLookAt.y = 0f;
            positionToLookAt.z = move.z;
            currentRotation = transform.rotation;
            var camera = GameObject.FindGameObjectWithTag("Top Down Camera");
            Debug.Log($"Camera is {camera.name} rot: {camera.transform.rotation.eulerAngles}");
            var matrix = Matrix4x4.Rotate(Quaternion.Euler(0f, camera.transform.rotation.eulerAngles.y, 0f));
            var skewed = matrix.MultiplyPoint3x4(positionToLookAt);
            var rot = Quaternion.LookRotation(skewed, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, turnRate * Time.deltaTime);
        }
    }

    void HandleMovement(CharacterController controller)
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector3 forward = transform.InverseTransformVector(Camera.main.transform.forward);
        Vector3 right = transform.InverseTransformVector(Camera.main.transform.right);
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        var forwardRelativeVerticalInput = verticalInput * forward;
        var rightRelativeVerticalInput = horizontalInput * right;

        //move = (forwardRelativeVerticalInput + rightRelativeVerticalInput).normalized;
        move = new Vector3(horizontalInput, 0f, verticalInput);

        velocity.y += gravity * Time.deltaTime;
        if (move.magnitude >= 0.1f)
        {
            speed = maxSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = minSpeed;
            }
            

            if (isAttacking)
            {
                speed = 0;
            }
            controller.Move(transform.forward * (speed + dashSpeed) * Time.deltaTime);
        }
        else
        {
            speed = 0;
        }
        controller.Move(velocity * Time.deltaTime);
    }

    void UpdateMoveSpeed()
    {
        maxSpeed = myStats.Movespeed.GetValue();
    }

    void OnAttack(int attackString)
    {
        isAttacking = true;
    }
    public void AttackFinish_AnimationEvent()
    {
        isAttacking = false;
    }
}
