using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private InputReader inputReader;

    //private GameInput playerInput;
    private Vector2 direction;
    public float speed = 4f;

    float turnSmoothTime = 0.1f;
    float turnSmootVelocity;


    Vector3 velocity;
    bool isGrounded;

    private void Awake()
    {
        inputReader.EnableGameplayInput();
        inputReader.MoveEvent += Movement;
    }

    private void Movement(Vector2 movement)
    {
        direction = movement;

    }

    // Update is called once per frame
    void Update()
    {

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmootVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
        }

        characterController.Move(velocity * Time.deltaTime);
    }


}