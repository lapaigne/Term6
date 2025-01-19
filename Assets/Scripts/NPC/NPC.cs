using UnityEngine;
using UnityEngine.AI;
public class NPC : MonoBehaviour
{
    // Current HP amount
    public int HP;
    
    // Shared maximum HP for each enemy of a specific kind
    public IntReference MaxHP;
    
    // Shared maximum movement speed for each enemy of a specific kind
    public FloatReference MaxSpeed;
    
    public Vector2 LookDirection { get; private set; }
    public Vector2 MoveDirection { get; private set; }
    
    public Vector2 LookTarget;
    public Vector2 MoveTarget;

    public NPCActions CurrentAction;

    private float _updateTimer = 0;
    private NavMeshAgent _agent;

    // test only
    public GameObject Player;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (_updateTimer > .5f)
        {
            // seek new target
            _agent.SetDestination(Player.transform.position);
        }
        _updateTimer += Time.deltaTime;
    }


}
