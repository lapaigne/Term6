using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput inputActions;
    private CharacterController characterController;
    private PlayerData playerData;
    private Vector2 rawInput;

    private Vector2 mouseWorldPosition;
    private Vector2 mouseDirection;
    
    [SerializeField]
    public Camera currentCamera;
    private void Awake()
    {
        playerData = GetComponent<PlayerData>();
        characterController = GetComponent<CharacterController>();
        inputActions = new PlayerInput();
        currentCamera = Camera.main;
    }

    private void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Interact.started += OnInteract;  
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Interact.canceled -= OnInteract;
        inputActions.Disable();
    }

    private void OnInteract(InputAction.CallbackContext ctx)
    { 
        RaycastHit2D hitInfo = Physics2D.Raycast(
            characterController.transform.position, 
            mouseDirection, 
            playerData.interactionDistance
            );
        
        if (hitInfo)
        {
            Debug.DrawLine(characterController.transform.position, mouseWorldPosition, Color.yellow, 10f);
            if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact(gameObject);
            }
        }
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        rawInput = ctx.ReadValue<Vector2>();
    }

    private void Update()
    {
        Vector2 movement = rawInput;
        movement.Normalize();
        movement *= playerData.speed * Time.deltaTime;
        characterController.Move(movement);

        mouseWorldPosition = currentCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseDirection = mouseWorldPosition - (Vector2)characterController.transform.position;

        if (mouseDirection.magnitude > 0.1f)
        {
            float rotationAngle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
        }
    }
}
