using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDebuggerActionExample : MonoBehaviour
{
    public InputAction exampleAction;

    // Start is called before the first frame update
    private void OnEnable()
    {
        exampleAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
