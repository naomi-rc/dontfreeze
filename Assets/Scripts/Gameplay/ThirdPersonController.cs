using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class ThirdPersonController : MonoBehaviour
{
    [SerializeField]
    private CharacterController characterController;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Transform mainCamera;

    [SerializeField]
    private InputReader inputReader;

    [SerializeField]
    private Transform groudCheckTransform;

    public float speed = 4f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    private float targetAngle = 0f;

    private float turnSmoothTime = 0.1f;
    private float turnSmootVelocity;

    private Vector2 movement;
    private Vector3 velocity;

    private bool jumpActivated;
    private bool isAttacking;

    private Collider[] colliderZone;


    private void Awake()
    {
        inputReader.EnableGameplayInput();
        inputReader.JumpEvent += ApplyJump;
        inputReader.AttackEvent += AttackEnemy;
        inputReader.MoveEvent += ApplyMovement;
    }

    private void OnDisable()
    {
        inputReader.JumpEvent -= ApplyJump;
        inputReader.MoveEvent -= ApplyMovement;
    }

    private void ApplyJump()
    {
        jumpActivated = true;
        if (Physics.OverlapSphere(groudCheckTransform.position, 0.3f).Length > 1)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -0.5f * gravity);
        }      
    }

    private void ApplyMovement(Vector2 value)
    {
        movement = value;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Gravity();

        if(Physics.OverlapSphere(groudCheckTransform.position, 0.3f).Length > 1)
        {
            animator.SetBool("isGrounded", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);

            if (jumpActivated)
            {
                animator.SetBool("isJumping", true);
                jumpActivated = false;
            }
        }
        else
        {
            animator.SetBool("isGrounded", false);
            animator.SetBool("isFalling", true);        
        }

        if (isAttacking)
        {
            isAttacking = false;
            animator.SetBool("isAttacking", true);
            foreach(var collider in colliderZone)
            {
                if (collider.gameObject.TryGetComponent<EnemyHealthController>(out EnemyHealthController enemyHealthController))
                {
                    enemyHealthController.TakeDamage(5);
                }
            }        
        } 
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }
    private void FixedUpdate()
    {
        colliderZone = Physics.OverlapSphere(this.transform.position, 2f);
    }

    private void LateUpdate()
    {
        Look();
    }

    private void Move()
    {
        float targetSpeed = (movement != Vector2.zero) ? speed : 0f;

        // We could implement 'accelerate to target speed'

        if (movement.magnitude >= 0.1f)
        {
            targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmootVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        Vector3 targetDirection = Quaternion.Euler(0.0f, targetAngle, 0.0f) * Vector3.forward;

        characterController.Move(targetDirection.normalized * (targetSpeed * Time.deltaTime) + (velocity * Time.deltaTime));
    }

    private void Gravity()
    {
        velocity.y += gravity * Time.deltaTime;
    }

    private void Look()
    {
        // We could customize cinemachine behavior here, if we want.
    }

    private void AttackEnemy()
    {
        isAttacking = true;
    }
}