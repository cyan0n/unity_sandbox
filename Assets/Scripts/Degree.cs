using UnityEngine;

public class Degree
{
    private float _value;

    public float Value
    {
        get => _value;
        set => _value = value;
    }

    public float Abs
    {
        get
        {
            float calc = _value % 360;
            return calc >= 0 ? calc : calc + 360;
        }
    }

    public Degree(float degree)
    {
        _value = degree;
    }

    public Degree(Vector2 vector): this(vector.ToDeg())
    { }

    public Degree(): this(0.0f)
    { }

    public static Degree AcuteDiff(Degree a, Degree b)
    {
        Degree alfa = a - b;
        Degree beta = b - a;
        return alfa.Abs < beta.Abs ? alfa.Abs : beta.Abs;
    }

    public static implicit operator Degree(float value) => new Degree(value);

    public static implicit operator Degree(Vector2 value) => new Degree(value.ToDeg());

    public static Degree operator -(Degree a, Degree b) => a.Value - b.Value;
    public static bool operator <(Degree a, Degree b) => a.Value < b.Value;

    public static bool operator <=(Degree a, Degree b) => a.Value <= b.Value;

    public static bool operator >(Degree a, Degree b) => a.Value > b.Value;

    public static bool operator >=(Degree a, Degree b) => a.Value >= b.Value;
}
