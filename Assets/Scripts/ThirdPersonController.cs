using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour, ThirdPersonInput.IGameplayActions
{
    public float speed = 6.0f;
    public float turnSmoothTime = 0.1f;
    public int index;

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
        this.direction = Round(context.ReadValue<Vector2>(), this.direction.x == 0 ? Vector2.right : Vector2.up);
    }

    private static Vector2 Round(Vector2 vector2)
    {
        return Round(vector2, Vector2.up);
    }

    private static Vector2 Round(Vector2 vector2, Vector2 priority)
    {
        if (vector2 == Vector2.zero)
        {
            return Vector2.zero;
        }

        if (vector2.x == vector2.y)
        {
            return priority == Vector2.up
                ? vector2.y > 0 ? Vector2.up : Vector2.down
                : vector2.x > 0 ? Vector2.right : Vector2.left;
        }

        if (vector2.x == 0 || Mathf.Abs(vector2.y) > Mathf.Abs(vector2.x))
        {
            return vector2.y > 0 ? Vector2.up : Vector2.down;
        }

        return vector2.x > 0 ? Vector2.right : Vector2.left;
    }
}
