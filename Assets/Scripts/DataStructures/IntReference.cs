using System;

[Serializable]
public class IntReference
{
    public bool UseConstant = true;
    public int ConstantValue;
    public IntVariable Variable;

    public int Value
    {
        get => UseConstant ? ConstantValue : Variable.Value;
    }

    public static implicit operator int(IntReference reference) => reference.Value;    
}