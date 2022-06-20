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

    private bool isJumping;
    private bool jumpActivated;

    private void Awake()
    {
        inputReader.EnableGameplayInput();
        inputReader.JumpEvent += ApplyJump;
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
       
        if (movement.sqrMagnitude < Mathf.Epsilon)
        {
            animator.SetBool("isMoving", false);
        } else
        {
            animator.SetBool("isMoving", true);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Gravity();

        if (Physics.OverlapSphere(groudCheckTransform.position, 0.3f).Length > 1)
        {
            animator.SetBool("isGrounded", true);
            isJumping = false;
            animator.SetBool("isJumping", false);

            animator.SetBool("isFalling", false);
            if (jumpActivated)
            {
                isJumping = true;
                animator.SetBool("isJumping", true);
                jumpActivated = false;
            }
        }
        else
        {
            animator.SetBool("isGrounded", false);

            if ((isJumping && velocity.y < 0f) || velocity.y < -2f)
            {
                animator.SetBool("isFalling", true);
            }
        }
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
}