using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour, ThirdPersonInput.IGameplayActions
{
    public float speed = 6.0f;
    public float turnSmoothTime = 0.1f;

    private CharacterController controller;
    private Vector2 direction;
    private Vector3 playerVelocity;
    private float turnSmoothVelocity;
    private bool isGrounded;

    private ThirdPersonInput input;

    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        if (input == null)
        {
            input = new ThirdPersonInput();
            input.Gameplay.SetCallbacks(this);
        }
        input.Gameplay.Enable();
    }

    private void OnDisable()
    {
        input.Gameplay.Disable();
    }
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (direction.magnitude >= 0.1f)
        {
            float currentSpeed = speed;
            float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            Vector3 moveDirection = Quaternion.Euler(0.0f, angle, 0.0f) * Vector3.forward;
            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        this.direction = context.ReadValue<Vector2>();
    }
}
