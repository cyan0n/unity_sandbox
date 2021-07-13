using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class HandTrigger : MonoBehaviour, ThirdPersonInput.IInteractionActions
{
    private Collider box = null;
    private ThirdPersonInput input;

    private bool canTakeTheBox = false;
    private bool holdingBox = false;
 
    //setUp Input
    private void OnEnable()
    {
        if (input == null)
        {
            input = new ThirdPersonInput();
            input.Interaction.SetCallbacks(this);
        }
        input.Interaction.Enable();
    }

    private void OnDisable()
    {
        input.Interaction.Disable();
    }

    private void OnTriggerEnter(Collider other)
    {
        canTakeTheBox = true;
        if (!holdingBox)
        {
            box = other;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == box)
        {
            box = null;
        }
            canTakeTheBox = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (holdingBox)
        {
            Vector3 posTmp = transform.position;
            box.transform.position = posTmp;
        }
    }

    public void OnTake(InputAction.CallbackContext context)
    {
        if (canTakeTheBox && context.performed && !holdingBox)
        {
            holdingBox = true;
        }else if (context.performed && holdingBox)
        {
            holdingBox = false;
        }
        Debug.Log("takee");
    }
}
