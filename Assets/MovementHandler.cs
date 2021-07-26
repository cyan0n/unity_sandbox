using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementHandler : MonoBehaviour
{
    public float speed = 6.0f;

    private GameplayInputReader inputReader;
    private CharacterController characterController;
    private FourWayMovement movement;
    private Vector3 direction;

    private void Awake()
    {
        this.movement = new FourWayMovement();
        this.inputReader = GetComponentInChildren<GameplayInputReader>();
        this.characterController = gameObject.GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        this.inputReader.moveEvent += OnMove;
    }

    private void OnDisable()
    {
        this.inputReader.moveEvent -= OnMove;
    }

    private void Update()
    {
        Move();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        this.direction = movement.ParseInput(context);
    }

    private void Move()
    {
        if (direction.magnitude >= 0.1f )
        {
            characterController.Move(direction * Time.deltaTime * speed);

            if (direction != Vector3.zero)
            {
                gameObject.transform.forward = direction;
            }
        }
    }
}
