using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float minSpeed = 3.5f;
    public float maxSpeed = 7f;
    public bool isDashing;
    public float dashSpeed;
    public float dashStartTime;
    public float dashDuration;
    public float speed;
    
    public float jumpSpeed = 3f;
    public float gravity = -20f;
    public float groundDistance = 0.1f;
    public Transform groundChecker;
    public LayerMask groundMask;
    bool isGrounded;
    Vector3 velocity;

    float turnSmoothness = 0.1f;
    float turnSmoothVelocity;

    CharacterCombat combat;
    public bool isAttacking = false;
    public bool isMoving = false;

    public event System.Action OnDash;

    InteractableScanner scanner;


    // Start is called before the first frame update
    void Start()
    {
        scanner = GetComponent<InteractableScanner>();
        combat = GetComponent<CharacterCombat>();
        combat.OnAttack += OnAttack;
        speed = maxSpeed;
        isDashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        CharacterController controller = GetComponent<CharacterController>();
        HandleMovement(controller);
        PlayerInteract();
        if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
        {
            OnStartDash();
        }
        if (isDashing)
        {
            if (Time.time - dashStartTime <= dashDuration)
            {
                isMoving = true;
                controller.Move(transform.forward * dashSpeed * Time.deltaTime);
            }
            else
            {
                OnEndDash();
            }
        }
    }

    void PlayerInteract()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (scanner.closestInteractable != null)
            {
                Debug.Log("Trying to interact...");
                scanner.closestInteractable.interacting = true;
            }
        }
    }

    void HandleMovement(CharacterController controller)
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

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
            
            if(!isDashing)
            {
                isMoving = true;
                if (isAttacking)
                {
                    speed /= 10;
                }
                controller.Move(move * speed * Time.deltaTime);
            }
        }
        else
        {
            speed = 0;
            isMoving = false;
        }
    }
    
    void OnStartDash()
    {
        isDashing = true;
        dashStartTime = Time.time;
        if(OnDash != null)
        {
            OnDash();
        }
    }
    public void OnEndDash()
    {
        isDashing = false;
        dashStartTime = 0;
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
