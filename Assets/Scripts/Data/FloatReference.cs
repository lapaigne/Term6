public class FloatReference
{
    public bool UseConstant = true;
    public float ConstantValue;
    public FloatVariable Variable;

    public float Value
    {
        get => UseConstant ? ConstantValue : Variable.Value;
    }

    public static implicit operator float(FloatReference reference) => reference.Value;
}