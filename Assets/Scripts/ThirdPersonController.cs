using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    public float speed = 6.0f;
    public float turnSmoothTime = 0.1f;


    private CharacterController controller;
    public CardinalDirection direction;
    private Vector3 playerVelocity;
    private float turnSmoothVelocity;


    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
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
            Vector3 move = direction.ToVector3();
            controller.Move(move * Time.deltaTime * speed);

            if (move != Vector3.zero)
            {
                gameObject.transform.forward = move;
            }
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
