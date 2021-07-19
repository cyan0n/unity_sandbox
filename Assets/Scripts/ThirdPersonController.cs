using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    public float speed = 6.0f;
    public float turnSmoothTime = 0.1f;


    private CharacterController controller;
    private FourWayMovement movement;
    private Vector3 direction;

    private void Awake()
    {
        movement = new FourWayMovement();
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (direction.magnitude >= 0.1f)
        {
            controller.Move(direction * Time.deltaTime * speed);

            if (direction != Vector3.zero)
            {
                gameObject.transform.forward = direction;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = movement.ParseInput(context);
    }

}
