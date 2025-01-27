using UnityEngine;
using UnityEngine.AI;
public partial class NPC : MonoBehaviour, IEntity
{
    public int CurrentHP { get; private set; }
    public float CurrentMoveSpeed { get; private set; }
    public Vector2 LookDirection { get; private set; }
    public Vector2 MoveDirection { get; private set; }

    [SerializeField]
    private string _displayName;
    public string DisplayName { get => _displayName; set => _displayName = value; }
    [SerializeField]
    private IntReference _maxHP;
    public IntReference MaxHP { get => _maxHP; set => _maxHP = value; }
    [SerializeField]
    private FloatReference _maxMoveSpeed;
    public FloatReference MaxSpeed { get => _maxMoveSpeed; set => _maxMoveSpeed = value; }

    public Vector2 LookTarget;
    public Vector2 MoveTarget;

    public NPCAction CurrentAction;
    public NPCRelation CurrentRelation;

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

        _agent.speed = MaxSpeed;
    }

    private NPCAction SwitchRelation() =>
    CurrentRelation switch
    {
        NPCRelation.Neutral => NPCAction.Idle,
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
