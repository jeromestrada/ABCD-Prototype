using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float minSpeed = 3.5f;
    public float maxSpeed = 7f;
    bool isDashing;
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
    bool isAttacking = false;
    public bool isMoving = false;


    // Start is called before the first frame update
    void Start()
    {
        combat = GetComponent<CharacterCombat>();
        combat.OnAttack += OnAttack;
        speed = 0;
        isDashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) // if already grounded, adjust the effect of gravity accordingly
        {
            velocity.y = -1f;
        }

        CharacterController controller = GetComponent<CharacterController>();

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        if(move.magnitude >= 0.1f)
        {
            speed = minSpeed;
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothness);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            if (Input.GetKeyDown(KeyCode.Space) && !isDashing)
            {
                OnStartDash();
            }
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = maxSpeed;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = minSpeed;
            }
            if (!isAttacking)
            {
                if (isDashing)
                {
                    if (Time.time - dashStartTime <= dashDuration)
                    {
                        isMoving = true;
                        controller.Move(move * dashSpeed * Time.deltaTime);
                    }
                    else
                    {
                        OnEndDash();
                    }
                }
                else
                {
                    isMoving = true;
                    controller.Move(move * speed * Time.deltaTime);
                }
            }
        }
        else
        {
            speed = 0f;
            isMoving = false;
        }
    }

    void OnAttack()
    {
        isAttacking = true;
    }

    void OnStartDash()
    {
        isDashing = true;
        dashStartTime = Time.time;
    }

    void OnEndDash()
    {
        isDashing = false;
        dashStartTime = 0;
    }

    public void AttackHit_AnimationEvent()
    {
        isAttacking = false;
    }
}
