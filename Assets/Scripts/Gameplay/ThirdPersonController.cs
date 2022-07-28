using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using AI;

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

    [SerializeField]
    private GameObject weaponHolder;

    [SerializeField]
    private InventoryDatabase inventoryDatabase;

    [SerializeField]
    private InventoryItemEventChannel onWeaponEquipEvent;

    public FloatReference speed;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public int unarmedDamage = 2;

    private float targetAngle = 0f;

    private float turnSmoothTime = 0.1f;
    private float turnSmootVelocity;

    private Vector2 movement;
    private Vector3 velocity;

    private bool jumpActivated;
    private bool isAttacking;
    private bool canAttackAgain = true;

    private bool isGrounded;

    private Collider[] colliderZone;


    private void Awake()
    {
        inputReader.EnableGameplayInput();
        inputReader.JumpEvent += ApplyJump;
        inputReader.AttackEvent += AttackEnemy;
        inputReader.MoveEvent += ApplyMovement;
        onWeaponEquipEvent.OnEventRaised += EquipWeapon;
        // Re-equip the current weapon on scene load 
        EquipWeapon(inventoryDatabase.currentWeapon);
    }

    private void OnDisable()
    {
        inputReader.JumpEvent -= ApplyJump;
        inputReader.MoveEvent -= ApplyMovement;
        onWeaponEquipEvent.OnEventRaised -= EquipWeapon;
    }

    private void ApplyJump()
    {
        jumpActivated = true;
        if(characterController.isGrounded)
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

        if (characterController.isGrounded)
        {
            animator.SetBool("isGrounded", true);
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);

            isGrounded = true;
        }

        if (jumpActivated && isGrounded)
        {
            animator.SetBool("isGrounded", false);
            animator.SetBool("isJumping", true);
            animator.SetBool("isFalling", true);

            canAttackAgain = false;
            Invoke("AttackAgain", 1);

            jumpActivated = false;
        }

        if (isAttacking && canAttackAgain)
        {
            isAttacking = false;
            animator.SetBool("isAttacking", true);
            FindObjectOfType<AudioManager>().Play("Fight");

            foreach (var collider in colliderZone)
            {
                if (collider.gameObject.TryGetComponent(out EnemyHealthController enemyHealthController))
                {
                    collider.gameObject.TryGetComponent(out EnemyBehavior enemyBehavior);
                    enemyBehavior.Hurt();
                    var damage = inventoryDatabase.currentWeapon != null ? inventoryDatabase.currentWeapon.damage : unarmedDamage;
                    FindObjectOfType<AudioManager>().Play(enemyBehavior.hurtSound);
                    enemyHealthController.TakeDamage(damage);
                }
            }
            canAttackAgain = false;
            Invoke("AttackAgain", 1);
        }
        else
        {
            animator.SetBool("isAttacking", false);
            isAttacking = false;
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
        float targetSpeed = (movement != Vector2.zero) ? speed.value : 0f;

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
    public void AttackAgain()
    {
        canAttackAgain = true;
    }

    private void EquipWeapon(InventoryItem item)
    {
        if (weaponHolder == null)
        {
            return;
        }

        foreach (Transform child in weaponHolder.transform)
        {
            Destroy(child.gameObject);
        }

        if (item != null)
        {
            Instantiate(item.model, weaponHolder.transform);
        }
    }
}