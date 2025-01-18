using UnityEngine;

public class NPC : MonoBehaviour
{
    // Current HP amount
    public int HP;
    
    // Shared maximum HP for each enemy of a specific kind
    public IntReference MaxHP;
    
    // Shared maximum movement speed for each enemy of a specific kind
    public FloatReference MaxSpeed;
    
    public Vector2 LookDirection {  get; private set; }
    public Vector2 MoveDirection { get; private set; }

    public NPCActions CurrentAction;
}
