using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameplayInputReader : MonoBehaviour, ThirdPersonInput.IGameplayActions
{
    public event UnityAction<InputAction.CallbackContext> moveEvent;

    public void OnMove(InputAction.CallbackContext context)
    {
        if (moveEvent != null)
        {
            moveEvent.Invoke(context);
        }
    }
}
