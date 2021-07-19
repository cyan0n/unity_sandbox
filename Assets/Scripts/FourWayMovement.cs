using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// TODO: Make Movement"Parser" interface/abstract class
public class FourWayMovement
{
    private CardinalDirection _direction = CardinalDirection.None;

    public Vector3 ParseInput(InputAction.CallbackContext context)
    {
        Vector2 vectorInput = context.ReadValue<Vector2>();
        if (vectorInput == Vector2.zero)
        {
            _direction = CardinalDirection.None;
            return Vector3.zero;
        }
        CardinalDirection movementCardinal = vectorInput.ToCardinalDirection();
        if (context.control.layout == ControlLayout.Stick &&
            (_direction == CardinalDirection.None ||
            Degree.AcuteDiff(vectorInput, _direction.ToVector2()) > 45))
        {
            _direction = movementCardinal.TogglePriority();
        }

        if (context.control.layout == ControlLayout.Key && movementCardinal != _direction)
        {
            if ((_direction & movementCardinal) != CardinalDirection.None)
            {
                _direction ^= movementCardinal;
            }
            else
            {
                _direction = movementCardinal;
            }
        }
        return _direction.ToVector3();
    }
}
