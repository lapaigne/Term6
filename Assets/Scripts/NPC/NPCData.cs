using UnityEngine;
using UnityEngine.AI;

public partial class NPC : MonoBehaviour
{
    public int currentHP;
    public float currentMoveSpeed;
    public Vector2 lookDirection;
    public Vector2 moveDirection;

    [SerializeField]
    public string displayName;
    [SerializeField]
    public IntReference maxHP;
    [SerializeField]
    public FloatReference maxSpeed;

    public Vector2 lookTarget;
    public Vector2 moveTarget;

    public NPCAction currentAction;
    public NPCRelation currentRelation;

    private float _updateTimer = 0;
    private NavMeshAgent _agent;

    // test only
    public GameObject Player;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        // passing data from custom editor to agent component

        _agent.speed = maxSpeed;
    }

    private NPCAction SwitchRelation() =>
    currentRelation switch
    {
        NPCRelation.Hostile => SetDestination(),
        _ => NPCAction.Idle,
    };

    private NPCAction SetDestination()
    {
        _agent.SetDestination(Player.transform.position);
        return NPCAction.Attack;
    }

    private void Update()
    {    
        if (_updateTimer > .5f)
        {
            SwitchRelation();
            _updateTimer = 0;
        }
        _updateTimer += Time.deltaTime;
    }
}
