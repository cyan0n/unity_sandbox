using System;
using UnityEngine;

[Flags]
public enum CardinalDirection
{
    None = 0,
    North = 1,
    East = 2,
    South = 4,
    West = 8,
    Longitude = North | South,
    Latitude = East | West,
    NorthEast = North | East,
    NorthWest = North | West,
    SoutEast = South | East,
    SouthWest = South | West,
}

public static class CardinalDirectionExtensionMethods
{
    public static Vector2 ToVector2(this CardinalDirection direction)
    {
        switch (direction)
        {
            case CardinalDirection.North:
            {
                return Vector2.up;
            }
            case CardinalDirection.East:
            {
                return Vector2.right;
            }
            case CardinalDirection.South:
            {
                return Vector2.down;
            }
            case CardinalDirection.West:
            {
                return Vector2.left;
            }
        }
        return Vector2.zero;
    }

    public static CardinalDirection ToCardinalDirection(this Vector2 vector)
    {
        CardinalDirection result = CardinalDirection.None;
        if (vector == Vector2.zero)
        {
            return result;
        }
        int direction_Idx = Mathf.FloorToInt(vector.Rotate(-45).ToDeg() / 45);
        if (direction_Idx == 0 || direction_Idx == 1 || direction_Idx == 2)
        {
            result |= CardinalDirection.North;
        }
        if (direction_Idx == 2 || direction_Idx == 3 || direction_Idx == 4)
        {
            result |= CardinalDirection.East;
        }
        if (direction_Idx == 4 || direction_Idx == 5 || direction_Idx == 6)
        {
            result |= CardinalDirection.South;
        }
        if (direction_Idx == 6 || direction_Idx == 7 || direction_Idx == 0)
        {
            result |= CardinalDirection.West;
        }

        return result;
    }

    public static CardinalDirection ToCardinalDirection(this Degree degree)
    {
        CardinalDirection result = CardinalDirection.None;
        int direction_Idx = Mathf.FloorToInt((degree - 45).Abs / 45);
        if (direction_Idx == 0 || direction_Idx == 1 || direction_Idx == 2)
        {
            result |= CardinalDirection.North;
        }
        if (direction_Idx == 2 || direction_Idx == 3 || direction_Idx == 4)
        {
            result |= CardinalDirection.East;
        }
        if (direction_Idx == 4 || direction_Idx == 5 || direction_Idx == 6)
        {
            result |= CardinalDirection.South;
        }
        if (direction_Idx == 6 || direction_Idx == 7 || direction_Idx == 0)
        {
            result |= CardinalDirection.West;
        }

        return result;
    }

    public static float ToDeg(this Vector2 vector)
    {
        float result = Mathf.Atan2(vector.x, vector.y) * Mathf.Rad2Deg;
        if (result < 0)
        {
            result += 360;
        }
        return result;
    }

    public static Vector2 Rotate(this Vector2 vector, float degrees)
    {
        float delta = degrees * Mathf.Deg2Rad;
        return new Vector2(
            vector.x * Mathf.Cos(delta) - vector.y * Mathf.Sin(delta),
            vector.x * Mathf.Sin(delta) + vector.y * Mathf.Cos(delta)
        );
    }
}