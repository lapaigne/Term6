using UnityEngine;

public interface IEntity
{
    public int CurrentHP { get; }
    public IntReference MaxHP { get; set; }
    public string DisplayName { get; set; }
    public Vector2 LookDirection { get; }
    public Vector2 MoveDirection { get; }
    public float CurrentMoveSpeed { get; }
    public FloatReference MaxSpeed { get; set; }
}