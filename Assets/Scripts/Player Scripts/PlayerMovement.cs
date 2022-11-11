using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector3 move;
    public Vector3 dashForward;
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
    bool isGrounded;
    Vector3 velocity;

    float turnSmoothness = 0.1f;
    float turnSmoothVelocity;

    PlayerCombat combat;
    public bool isAttacking = false;
    public bool isMoving = false;

    InteractableScanner scanner;


    // Start is called before the first frame update
    void Start()
    {
        scanner = GetComponent<InteractableScanner>();
        combat = GetComponent<PlayerCombat>();
        PlayerCombat.OnAttack += OnAttack;
        speed = maxSpeed;
        dashSpeed = 0;
        dashForward = Vector3.zero;
        IsDashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        CharacterController controller = GetComponent<CharacterController>();
        HandleMovement(controller);
    }

    void HandleMovement(CharacterController controller)
    {
        move = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        
        if (move.magnitude >= 0.1f)
        {
            speed = maxSpeed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = minSpeed;
            }
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothness);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            isMoving = true;
            if (isAttacking)
            {
                speed = 0;
            }
            controller.Move(move * (speed + dashSpeed) * Time.deltaTime);
        }
        else
        {
            speed = 0;
            isMoving = false;
        }
        
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
