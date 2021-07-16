using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour, ThirdPersonInput.IGameplayActions
{
    public float speed = 6.0f;
    public float turnSmoothTime = 0.1f;


    private CharacterController controller;
    public CardinalDirection direction;
    private Vector3 playerVelocity;
    private float turnSmoothVelocity;

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
        Vector2 directionVector = this.direction.ToVector2();
        if (directionVector.magnitude >= 0.1f)
        {
            float currentSpeed = speed;
            float targetAngle = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);

            Vector3 moveDirection = Quaternion.Euler(0.0f, angle, 0.0f) * Vector3.forward;
            controller.Move(moveDirection * currentSpeed * Time.deltaTime);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 vectorInput = context.ReadValue<Vector2>();
        if (vectorInput == Vector2.zero)
        {
            this.direction = CardinalDirection.None;
            return;
        }
        // TODO: Create ControlLayout enumerator
        CardinalDirection movementCardinal = vectorInput.ToCardinalDirection();
        if (context.control.layout == "Stick" && (this.direction == CardinalDirection.None || Degree.AcuteDiff(vectorInput, this.direction.ToVector2()) > 45))
        {
            this.direction = movementCardinal.TogglePriority();
        }

        if (context.control.layout == "Key" && movementCardinal != this.direction)
        {
            if ((this.direction & movementCardinal) != 0)
            {
                this.direction ^= movementCardinal;
            }
            else
            {
                this.direction = movementCardinal;
            }
        }

    }

}
